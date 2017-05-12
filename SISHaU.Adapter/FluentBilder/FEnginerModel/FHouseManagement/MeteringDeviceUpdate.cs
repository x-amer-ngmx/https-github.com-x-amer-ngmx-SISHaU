namespace SISHaU.Adapter.FluentBilder.FEnginerModel.FHouseManagement
{
    public class MeteringDeviceUpdate : BaseModel<HouseManagementModel>
    {/*
        private importMeteringDeviceDataRequestMeteringDeviceDeviceDataToUpdate DeviceData { get; set; }
        private importMeteringDeviceDataRequestMeteringDevice MeteringDevices { get; set; }*/

        public MeteringDeviceUpdate(HouseManagementModel baseModel/*, importMeteringDeviceDataRequestMeteringDevice meteringDevices*/) : base(baseModel)
        {/*
            MeteringDevices = meteringDevices;
            DeviceData = new importMeteringDeviceDataRequestMeteringDeviceDeviceDataToUpdate();*/
        }
    }
}
