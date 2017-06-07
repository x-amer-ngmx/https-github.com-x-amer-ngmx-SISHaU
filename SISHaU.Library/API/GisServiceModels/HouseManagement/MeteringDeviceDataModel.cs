using System;
using System.Collections.Generic;
using Integration.Base;
using Integration.HouseManagement;
using Integration.HouseManagementService;
using Integration.HouseManagementServiceAsync;
using Integration.NsiBase;
using ImportResult = Integration.HouseManagement.ImportResult;
using SISHaU.Library.API;

namespace ServiceHelperHost.Library.GisServiceModel.HouseManagement
{
    public class MeteringDeviceDataModel : Requester<HouseManagementPortsTypeClient, HouseManagementPortsTypeAsyncClient>
    {
        private importMeteringDeviceDataRequest GisRequest { get; }
        private string FiasHouseGuid { get; }
        private List<importMeteringDeviceDataRequestMeteringDevice> MeteringDevice { get; set; }

        public MeteringDeviceDataModel(string fiasHouseGuid){
            FiasHouseGuid = fiasHouseGuid;
            GisRequest = GenerateGenericType<importMeteringDeviceDataRequest>();
            MeteringDevice = new List<importMeteringDeviceDataRequestMeteringDevice>();
        }

        public ImportResult ProcessRequest()
        {
            if (!string.IsNullOrEmpty(FiasHouseGuid))
            {
                GisRequest.FIASHouseGuid = FiasHouseGuid;
            }

            GisRequest.MeteringDevice = MeteringDevice.ToArray();

            return ProcessRequest<ImportResult>(GisRequest);
        }

        public void AddMeteringDevice(
            DateTime commissioningDate, DateTime factorySealDate, DateTime firstVerificationDate, DateTime installationDate,
            string meteringDeviceModel, string meteringDeviceNumber, string meteringDeviceStamp, string remoteMeteringInfo,
            bool pressureSensor, bool remoteMeteringMode, bool temperatureSensor, bool notLinkedWithMetering,
            nsiRef verificationInterval)
        {
            MeteringDevice.Add(new importMeteringDeviceDataRequestMeteringDevice
            {
                TransportGUID = TransportGuid,
                DeviceDataToCreate = new MeteringDeviceFullInformationType
                {
                    BasicChatacteristicts = GetBasicCharacteristics(
                        commissioningDate, factorySealDate, firstVerificationDate, installationDate,
                        meteringDeviceModel, meteringDeviceNumber, meteringDeviceStamp, remoteMeteringInfo,
                        pressureSensor, remoteMeteringMode, temperatureSensor, notLinkedWithMetering,verificationInterval),
                    LinkedWithMetering = new MeteringDeviceFullInformationTypeLinkedWithMetering
                    {
                        InstallationPlace = MeteringDeviceFullInformationTypeLinkedWithMeteringInstallationPlace.@in,
                        LinkedMeteringDeviceVersionGUID = new string[] {}
                    },
                    MunicipalResourceEnergy = new MunicipalResourceElectricType
                    {
                        MeteringValueT1 = 0,
                        MeteringValueT2 = 0,
                        MeteringValueT3 = 0,
                        ReadingsSource = "",
                        TransformationRatio = 0
                    },
                    MunicipalResourceNotEnergy = new MunicipalResourceNotElectricType
                    {
                        MeteringValue = 0,
                        MunicipalResource = new nsiRef
                        {
                            Code = "",
                            GUID = "",
                            Name = ""
                        },
                        ReadingsSource = ""
                    },
                    NotLinkedWithMetering = notLinkedWithMetering
                }
            });
        }

        public MeteringDeviceBasicCharacteristicsType GetBasicCharacteristics(
            DateTime commissioningDate, DateTime factorySealDate, DateTime firstVerificationDate, DateTime installationDate,
            string meteringDeviceModel, string meteringDeviceNumber, string meteringDeviceStamp, string remoteMeteringInfo,
            bool pressureSensor, bool remoteMeteringMode, bool temperatureSensor, bool notLinkedWithMetering,
            nsiRef verificationInterval)
        {
            return new MeteringDeviceBasicCharacteristicsType
            {
                ApartmentHouseDevice = new MeteringDeviceBasicCharacteristicsTypeApartmentHouseDevice
                {
                    AccountGUID = new string[] {}
                },
                CollectiveApartmentDevice = new MeteringDeviceBasicCharacteristicsTypeCollectiveApartmentDevice
                {
                    AccountGUID = new string[] {},
                    PremiseGUID = ""
                },
                CollectiveDevice = new MeteringDeviceBasicCharacteristicsTypeCollectiveDevice
                {
                    Certificate = new AttachmentType[] {},
                    PressureSensingElementInfo = "",
                    ProjectRegistrationNode = new AttachmentType[] {},
                    TemperatureSensingElementInfo = ""
                },
                LivingRoomDevice = new MeteringDeviceBasicCharacteristicsTypeLivingRoomDevice
                {
                    AccountGUID = new string[] {},
                    LivingRoomGUID = new string[] {}
                },
                NonResidentialPremiseDevice = new MeteringDeviceBasicCharacteristicsTypeNonResidentialPremiseDevice
                {
                    AccountGUID = new string[] {},
                    PremiseGUID = ""
                },
                ResidentialPremiseDevice = new MeteringDeviceBasicCharacteristicsTypeResidentialPremiseDevice
                {
                    AccountGUID = new string[] {},
                    PremiseGUID = ""
                },
                CommissioningDate = commissioningDate,
                FactorySealDate = factorySealDate,
                FirstVerificationDate = firstVerificationDate,
                InstallationDate = installationDate,
                MeteringDeviceModel = meteringDeviceModel,
                MeteringDeviceNumber = meteringDeviceNumber,
                MeteringDeviceStamp = meteringDeviceStamp,
                RemoteMeteringInfo = remoteMeteringInfo,
                PressureSensor = pressureSensor,
                RemoteMeteringMode = remoteMeteringMode,
                TemperatureSensor = temperatureSensor,
                VerificationInterval = verificationInterval
            };
        }
    }
}
