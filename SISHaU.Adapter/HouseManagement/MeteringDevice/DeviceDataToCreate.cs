using Integration.HouseManagement;
using SISHaU.Adapter.NsiCommon;
using SISHaU.Library.Util;

namespace SISHaU.Adapter.HouseManagement.MeteringDevice
{
    public class DeviceDataToCreate : GisUtil
    {
        private importMeteringDeviceDataRequestMeteringDevice MeteringDevice { get; set; }
        private NsiCommonDataModel NsiCommonModel { get; }

        public DeviceDataToCreate(string transportGuid = null)
        {
            MeteringDevice = new importMeteringDeviceDataRequestMeteringDevice{
                TransportGUID = string.IsNullOrEmpty(transportGuid) ? TransportGuid : transportGuid,
                DeviceDataToCreate = new MeteringDeviceFullInformationType
                {
                    NotLinkedWithMetering = true,
                    NotLinkedWithMeteringSpecified = true
                }
            };

            NsiCommonModel = new NsiCommonDataModel();
        }
        
        public DeviceDataToCreate SetBasicCharacteristics(BasicCharacteristics basicCharacteristics)
        {
            MeteringDevice.DeviceDataToCreate.BasicChatacteristicts = basicCharacteristics.Build();
            return this;
        }

        public importMeteringDeviceDataRequestMeteringDevice Build()
        {
            return MeteringDevice;
        }

        public DeviceDataToCreate SetMeteringBaseValue(string resourceName, string meteringValue)
        {
            MeteringDevice.DeviceDataToCreate.MunicipalResourceNotEnergy = new MunicipalResourceNotElectricType{
                MunicipalResource = NsiCommonModel.GetMeteringDeviceMunicipalResource(resourceName),
                MeteringValue = decimal.Parse(meteringValue)
            };
            return this;
        }
    }
}
