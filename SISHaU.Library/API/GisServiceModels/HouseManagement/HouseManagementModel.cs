using System.Collections.Generic;
using System.Linq;
using Integration.Base;
using Integration.HouseManagement;
using Integration.HouseManagementService;
using Integration.HouseManagementServiceAsync;
using SISHaU.Library.API.GisServiceModels.HouseManagement.Account;
using SISHaU.Library.API.GisServiceModels.HouseManagement.MeteringDevice;
using SISHaU.Library.API.GisServiceModels.HouseManagement.SupplyResourceContract;
using SISHaU.Library.API.GisServiceModels.HouseRso;
using ImportResult = Integration.HouseManagement.ImportResult;

namespace SISHaU.Library.API.GisServiceModels.HouseManagement
{

    public class MeteringDeviceResponseStruct
    {
        public getStateRequest Request { get; set; }
        public string TransportGuid { get; set; }
    }

    public class HouseManagementModel : Requester<HouseManagementPortsTypeClient, HouseManagementPortsTypeAsyncClient>
    {
        public IDictionary<importHouseRSORequest, ImportResult> HouseRsoImport { get; set; }
        public IDictionary<exportHouseRequest, exportHouseResult> HouseRsoExport { get; set; }
        public IDictionary<importAccountRequest, ImportResult> AccountImport { get; set; }
        public IDictionary<exportAccountRequest, exportAccountResult> AccountExport { get; set; }
        public IDictionary<importMeteringDeviceDataRequest, ImportResult> MeteringDeviceImport { get; set; }
        public IDictionary<exportMeteringDeviceDataRequest, exportMeteringDeviceDataResult> MeteringDeviceExport { get; set; }
        public IDictionary<importSupplyResourceContractRequest, ImportResult> SupplyResourceContractImport { get; set; }
        public IDictionary<exportSupplyResourceContractRequest, exportSupplyResourceContractResult> SupplyResourceContractExport { get; set; }


        public HouseRsoModel<exportHouseRequest, exportHouseResult> ExportHouseRso()
        {
            if (HouseRsoExport == null)
                HouseRsoExport = new Dictionary<exportHouseRequest, exportHouseResult>();
            return new HouseRsoModel<exportHouseRequest, exportHouseResult>(this, HouseRsoExport);
        }

        public HouseRsoModel<importHouseRSORequest, ImportResult> ImportHouseRso()
        {
            if (null == HouseRsoImport)
                HouseRsoImport = new Dictionary<importHouseRSORequest, ImportResult>();
            return new HouseRsoModel<importHouseRSORequest, ImportResult>(this, HouseRsoImport);
        }

        public AccountModel<importAccountRequest, ImportResult> ImportAccount()
        {
            if (null == AccountImport)
                AccountImport = new Dictionary<importAccountRequest, ImportResult>();
            return new AccountModel<importAccountRequest, ImportResult>(this, AccountImport);
        }

        public AccountModel<exportAccountRequest, exportAccountResult> ExportAccount()
        {
            if (null == AccountExport)
                AccountExport = new Dictionary<exportAccountRequest, exportAccountResult>();
            return new AccountModel<exportAccountRequest, exportAccountResult>(this, AccountExport);
        }

        public MeteringDeviceModel<importMeteringDeviceDataRequest, ImportResult> ImportMeteringDevice()
        {
            if (null == MeteringDeviceImport)
                MeteringDeviceImport = new Dictionary<importMeteringDeviceDataRequest, ImportResult>();
            return new MeteringDeviceModel<importMeteringDeviceDataRequest, ImportResult>(this, MeteringDeviceImport);
        }

        public MeteringDeviceModel<exportMeteringDeviceDataRequest, exportMeteringDeviceDataResult> ExportMeteringDevice()
        {
            if (null == MeteringDeviceImport)
                MeteringDeviceExport = new Dictionary<exportMeteringDeviceDataRequest, exportMeteringDeviceDataResult>();
            return new MeteringDeviceModel<exportMeteringDeviceDataRequest, exportMeteringDeviceDataResult>(this, MeteringDeviceExport);
        }

        public SupplyResourceContractModel<importSupplyResourceContractRequest, ImportResult> ImportSupplyResourceContract()
        {
            if(SupplyResourceContractImport == null)
                SupplyResourceContractImport = new Dictionary<importSupplyResourceContractRequest, ImportResult>();
            return new SupplyResourceContractModel<importSupplyResourceContractRequest, ImportResult>(this, SupplyResourceContractImport);
        }

        public SupplyResourceContractModel<exportSupplyResourceContractRequest, exportSupplyResourceContractResult> ExportSupplyResourceContract()
        {
            if(SupplyResourceContractExport == null)
                SupplyResourceContractExport = new Dictionary<exportSupplyResourceContractRequest, exportSupplyResourceContractResult>();
            return new SupplyResourceContractModel<exportSupplyResourceContractRequest, exportSupplyResourceContractResult>(this, SupplyResourceContractExport);
        }

        /*public IList<MeteringDeviceResponseStruct> Async()
        {
            return MeteringDeviceImport.Select(t => new MeteringDeviceResponseStruct
            {
                Request = BeginProcessRequest(t.Value),
                TransportGuid = t.Value.MeteringDevice.Select(tg => $"'{tg.TransportGUID}'").Aggregate((a, b) => $"{a},{b}")
            }).ToList();
        }*/

        public IList<ImportResult> GetState(IList<getStateRequest> getState)
        {
            return getState.Select(ProcessAsyncMessageState<ImportResult>).ToList();
        }

        private void SyncMethod<T, TU>(IDictionary<T, TU> syncData, bool bClearPool) where TU : class
        {
            if (syncData == null || !syncData.Any()) return;
            foreach (var syncDataElement in syncData.ToArray()){
                syncData[syncDataElement.Key] = ProcessRequest<TU>(syncDataElement.Key);
            }
            if(bClearPool) syncData.Clear();
        }

        /// <summary>
        /// Надо придумать изящный метод, который позволит делать поочерёдное выполнение каждого запроса в словаре
        /// </summary>
        /// <param name="clearPool"></param>
        /// <returns></returns>
        public HouseManagementModel Sync(bool clearPool = false)
        {
            SyncMethod(HouseRsoImport, clearPool);
            SyncMethod(HouseRsoImport, clearPool);
            SyncMethod(HouseRsoExport, clearPool);
            SyncMethod(AccountImport, clearPool);
            SyncMethod(AccountExport, clearPool);
            SyncMethod(MeteringDeviceImport, clearPool);
            SyncMethod(MeteringDeviceExport, clearPool);
            SyncMethod(SupplyResourceContractImport, clearPool);
            SyncMethod(SupplyResourceContractExport, clearPool);

            return this;
        }
    }
}
