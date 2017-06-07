using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
//using FluentNHibernate.Mapping;
using Integration.Base;
using Integration.HouseManagement;
using Integration.HouseManagementService;
using Integration.HouseManagementServiceAsync;
using Integration.NsiBase;
using SISHaU.Library.API;

namespace SISHaU.Adapter.HouseManagement
{
    public class HouseRsoDataModel : Requester<HouseManagementPortsTypeClient, HouseManagementPortsTypeAsyncClient>
    {
        //private readonly NsiCommonDataModel _nsiModel;
        public Dictionary<string, string> CommonResultGuids { get; set; }

        public exportHouseResult ExportHouse(string fiasGuid)
        {
            var request = GenerateGenericType<exportHouseRequest>();
            request.FIASHouseGuid = fiasGuid;
            return ProcessRequest<exportHouseResult>(request);
        }

        public getStateRequest BeginExportHouse(string fiasGuid)
        {
            var request = GenerateGenericType<exportHouseRequest>();
            request.FIASHouseGuid = fiasGuid;
            return BeginProcessRequest(request);
        }
        
        public HouseRsoDataModel(string sessionNumber = null)
        {
            //_nsiModel = new NsiCommonDataModel();

            var cfg = new MapperConfiguration(c => {
                c.CreateMap<HouseBasicExportType, HouseBasicRSOType>();
                c.CreateMap<ApartmentHouse, importHouseRSORequestApartmentHouse>();
                c.CreateMap<LivingHouse, importHouseRSORequestLivingHouse>();
            });

            InitMapper(cfg);

            if (string.IsNullOrEmpty(sessionNumber)) return;
            SessionGuid = sessionNumber;
        }

        private HouseBasicRSOType GetBasicCharacteristics(string fiasHouseGuid, string oktmoCode)
        {
            return new HouseBasicRSOType {
                //No_RSO_GKN_EGRP_Registered = true,
                //CadastralNumber = "25:04:110018:74",
                FIASHouseGuid = fiasHouseGuid,
                OKTMO = new OKTMORefType {code = oktmoCode},
                OlsonTZ = Nsi.GetOlsonTimeZone()
            };
        }

        public Integration.HouseManagement.ImportResult AddApartmentHouse(string fiasHouseGuid, string oktmoCode, Dictionary<string, decimal> residentialPremises, string entrance, string entranceGuid = null)
        {
            var apartmentHouse = new ApartmentHouse(GetBasicCharacteristics(fiasHouseGuid, oktmoCode), true);

            foreach (var rp in residentialPremises){
                apartmentHouse.AddResidentalPremise(rp.Key, rp.Value, entrance);
            }

            /*if (string.IsNullOrEmpty(entranceGuid)){
                apartmentHouse.AddEntrance(entrance, entranceGuid);
            }*/

            var request = GenerateGenericType<importHouseRSORequest>();
            request.ApartmentHouse = UtilMapper.Map<importHouseRSORequestApartmentHouse>(apartmentHouse.Build());
            var result = ProcessRequest<Integration.HouseManagement.ImportResult>(request);

            CommonResultGuids = apartmentHouse.GetCommonResultGuids(result.CommonResult);

            return result;
        }

        public Integration.HouseManagement.ImportResult UpdateApartmentHouse(exportHouseResultType exportHouse)
        {
            var apartmentHouse = new ApartmentHouse(GetBasicCharacteristics(
                exportHouse.ApartmentHouse.BasicCharacteristicts.FIASHouseGuid, 
                exportHouse.ApartmentHouse.BasicCharacteristicts.OKTMO.code), 
                true);

            foreach (var ahrp in exportHouse.ApartmentHouse.ResidentialPremises){
                apartmentHouse.TerminateResidentalPremise(exportHouse.ModificationDate.AddHours(1), ahrp.PremisesGUID);
            }

            var request = GenerateGenericType<importHouseRSORequest>();
            request.ApartmentHouse = UtilMapper.Map<importHouseRSORequestApartmentHouse>(apartmentHouse.Build());
            return ProcessRequest<Integration.HouseManagement.ImportResult>(request);
        }

        public Integration.HouseManagement.ImportResult AddLivingHouse(string fiasHouseGuid, string oktmoCode, Dictionary<string, decimal> premises, bool isBlockingHouse = false, bool isUpdating = false)
        {
            var livingHouse = new LivingHouse(GetBasicCharacteristics(fiasHouseGuid, oktmoCode), isUpdating);

            foreach (var lr in premises)
            {
                if (!isBlockingHouse)
                    livingHouse.AddLivingRoom(lr.Key, lr.Value);
                else
                    livingHouse.AddBlockPremise(lr.Key, lr.Value, Nsi.GetPremiseCharacteristic("Отдельная квартира"));
            }
            
            var request = GenerateGenericType<importHouseRSORequest>();
            request.LivingHouse = UtilMapper.Map<importHouseRSORequestLivingHouse>(livingHouse.Build());
            var result = ProcessRequest<Integration.HouseManagement.ImportResult>(request);

            //CommonResultGuids = livingHouse.GetCommonResultGuids(result.CommonResult);

            return result;
        }
        
        public getStateResult EndProcessRequest(string messageGuid)
        {
            return ProcessAsyncMessageState<getStateResult>(new getStateRequest
            {
                MessageGUID = messageGuid
            });
        }
    }

    public class ApartmentHouse : importHouseRSORequestApartmentHouse
    {
        private readonly Dictionary<string, importHouseRSORequestApartmentHouseResidentialPremises> _residentialPremises;
        private readonly List<EntranceRSOType> _entrances;
        //private Dictionary<string, string> PremisesGuids { get; }

        public ApartmentHouse(HouseBasicRSOType basicCharacteristics, bool bUpdate = false)
        {
            var apartmentHouse = new HouseRSOType {
                TransportGUID = Guid.NewGuid().ToString(),
                BasicCharacteristicts = basicCharacteristics
            };

            if (bUpdate)
                ApartmentHouseToUpdate = apartmentHouse;
            else
                ApartmentHouseToCreate = apartmentHouse;

            _residentialPremises = new Dictionary<string, importHouseRSORequestApartmentHouseResidentialPremises>();
            _entrances = new List<EntranceRSOType>();
        }

        /// <summary>
        /// При создании помещения сначала у него есть только номер и TransportGuid - после импорта добавляю уже Guid и UniqueNumber
        /// Делается для того, чтобы не посылать запрос уже после импорта на информацию о новых помещениях
        /// </summary>
        /// <param name="commonResults"></param>
        public Dictionary<string, string> GetCommonResultGuids(ImportResultCommonResult[] commonResults)
        {
            if(null == commonResults)
                return new Dictionary<string, string>();

            return commonResults
                .Where(cr => cr.Error == null && _residentialPremises.ContainsKey(cr.TransportGUID))
                .ToDictionary(cr => cr.TransportGUID,
                    cr =>
                        _residentialPremises[cr.TransportGUID]
                            .ResidentialPremisesToUpdate?.PremisesNum +
                        _residentialPremises[cr.TransportGUID]
                            .ResidentialPremisesToCreate?.PremisesNum + ";" + cr.GUID + ";" + cr.UniqueNumber);
        }

        public void TerminateResidentalPremise(DateTime terminationDate, string premiseGuid)
        {
            var rp = new ResidentialPremisesRSOType
            {
                TransportGUID = Guid.NewGuid().ToString(),
                //No_RSO_GKN_EGRP_Registered = true,
                TerminationDate = terminationDate,
                TerminationDateSpecified = true,
                PremisesGUID = premiseGuid,
                PremisesGUIDSpecified = true,
                EntranceNum = "0",
                EntranceNumSpecified = true,
                PremisesNum = "0"
            };

            _residentialPremises.Add(rp.TransportGUID, new importHouseRSORequestApartmentHouseResidentialPremises {
                ResidentialPremisesToUpdate = rp
            });
        }

        public void AddResidentalPremise(string premiseNumber, decimal? totalArea = null, string entranceNum = null, string premiseGuid = null)
        {
            var rp = new ResidentialPremisesRSOType{

                TransportGUID = Guid.NewGuid().ToString(),
                //No_RSO_GKN_EGRP_Registered = true,
                PremisesNum = premiseNumber
            };

            if (null != totalArea){

                rp.TotalArea = totalArea.Value;
                rp.TotalAreaSpecified = true;
            }

            if (!string.IsNullOrEmpty(entranceNum)){

                rp.EntranceNum = entranceNum;
                rp.EntranceNumSpecified = true;
            }
            
            if (!string.IsNullOrEmpty(premiseGuid)){

                rp.PremisesGUID = premiseGuid;
                rp.PremisesGUIDSpecified = true;

                _residentialPremises.Add(rp.TransportGUID, new importHouseRSORequestApartmentHouseResidentialPremises{
                    ResidentialPremisesToUpdate = rp
                });

                return;
            }

            _residentialPremises.Add(rp.TransportGUID, new importHouseRSORequestApartmentHouseResidentialPremises{
                ResidentialPremisesToCreate = rp
            });
        }

        public void AddEntrance(string entranceNumber, string entranceGuid = null)
        {
            var entranceObj = new EntranceRSOType{
                TransportGUID = Guid.NewGuid().ToString(),
                EntranceNum = entranceNumber
            };

            if (!string.IsNullOrEmpty(entranceGuid))
            {
                entranceObj.EntranceGUID = entranceGuid;
                entranceObj.EntranceGUIDSpecified = true;
            }

            _entrances.Add(entranceObj);
        }

        public importHouseRSORequestApartmentHouse Build()
        {
            ResidentialPremises = _residentialPremises.Values.ToArray();

            if (!_entrances.Any()) return this;

            EntranceToUpdate = _entrances.Where(e => e.EntranceGUIDSpecified).Select(e => e).ToArray();
            EntranceToCreate = _entrances.Where(e => !e.EntranceGUIDSpecified).Select(e => e).ToArray();

            return this;
        }
    }
    
    public class LivingHouse : importHouseRSORequestLivingHouse
    {
        private Dictionary<string, RoomRSOType> _livingRooms;
        private Dictionary<string, importHouseRSORequestLivingHouseBlocks> _lhBlocks;
        
        public LivingHouse(HouseBasicRSOType basicCharacteristics, bool isUpdating)
        {
            var livingHouse = new HouseRSOType
            {
                BasicCharacteristicts = basicCharacteristics,
                TransportGUID = Guid.NewGuid().ToString()
            };

            if (isUpdating)
                LivingHouseToUpdate = livingHouse;
            else
                LivingHouseToCreate = livingHouse;
        }

        public void AddBlockPremise(string blockNumber, decimal square, nsiRef premiseCharacteristic)
        {
            var block = new importHouseRSORequestLivingHouseBlocks
            {
                BlockToCreate = new importHouseRSORequestLivingHouseBlocksBlockToCreate
                {
                    TransportGUID = Guid.NewGuid().ToString(),
                    BlockNum = blockNumber,
                    //No_RSO_GKN_EGRP_Registered = true,
                    TotalArea = square,
                    PremisesCharacteristic = premiseCharacteristic
                }
            };

            if (null == _lhBlocks)
                _lhBlocks = new Dictionary<string, importHouseRSORequestLivingHouseBlocks>();

            _lhBlocks.Add(block.BlockToCreate.TransportGUID, block);
        }

        public void AddLivingRoom(string roomNumber, decimal square, string livingRoomGuid = null, DateTime? terminationDate = null)
        {
            var livingRoom = new RoomRSOType
            {
                TransportGUID = Guid.NewGuid().ToString(),
                RoomNumber = roomNumber,
                //No_RSO_GKN_EGRP_Registered = true,
                
            };
            
            if (!string.IsNullOrEmpty(livingRoomGuid))
            {
                livingRoom.LivingRoomGUID = livingRoomGuid;
                livingRoom.LivingRoomGUIDSpecified = true;
            }

            if (null != terminationDate)
            {
                livingRoom.TerminationDate = terminationDate.Value;
                livingRoom.TerminationDateSpecified = true;
            }

            if(null == _livingRooms)
                _livingRooms = new Dictionary<string, RoomRSOType>();

            _livingRooms.Add(livingRoom.TransportGUID, livingRoom);
        }

        /// <summary>
        /// При создании помещения сначала у него есть только номер и TransportGuid - после импорта добавляю уже Guid и UniqueNumber
        /// Делается для того, чтобы не посылать запрос уже после импорта на информацию о новых помещениях
        /// </summary>
        /// <param name="commonResults"></param>
        public Dictionary<string, string> GetCommonResultGuids(ImportResultCommonResult[] commonResults)
        {
            if (null == commonResults)
                return new Dictionary<string, string>();

            return commonResults
                .Where(cr => cr.Error == null && _livingRooms.ContainsKey(cr.TransportGUID))
                .ToDictionary(cr => cr.TransportGUID,
                    cr =>
                        _livingRooms[cr.TransportGUID]
                            .RoomNumber + cr.GUID + cr.UniqueNumber);
        }

        public importHouseRSORequestLivingHouse Build()
        {
            if (null != _lhBlocks)
            {
                Blocks = _lhBlocks.Select(b => b.Value).ToArray();

                return this;
            }

            if (null == _livingRooms) return this;

            LivingRoomToCreate =
                _livingRooms.Where(lr => !lr.Value.LivingRoomGUIDSpecified).Select(lr => lr.Value).ToArray();
            LivingRoomToUpdate =
                _livingRooms.Where(lr => lr.Value.LivingRoomGUIDSpecified).Select(lr => lr.Value).ToArray();

            return this;
        }
    }
}
