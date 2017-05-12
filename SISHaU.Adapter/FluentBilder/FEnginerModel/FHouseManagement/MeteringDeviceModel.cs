namespace SISHaU.Adapter.FluentBilder.FEnginerModel.FHouseManagement
{
    public class MeteringDeviceModel : BaseModel<HouseManagementModel>
    {
        private string/*importMeteringDeviceDataRequestMeteringDevice*/ MeteringDevices { get; }
        private string FiasHouseGuid { get; }

        public MeteringDeviceModel(HouseManagementModel baseModel, string fiasHouseGuid) : base(baseModel)
        {
            FiasHouseGuid = fiasHouseGuid;
            //MeteringDevices = new importMeteringDeviceDataRequestMeteringDevice();
        }

        public MeteringDeviceCreate Create()
        {
            return new MeteringDeviceCreate(BaseModelEntity/*, MeteringDevices*/);
        }

        public MeteringDeviceUpdate Update()
        {
            return new MeteringDeviceUpdate(BaseModelEntity/*, MeteringDevices*/);
        }

        public MeteringDeviceExport Export()
        {
            return new MeteringDeviceExport(BaseModelEntity).ByFiasGuid(FiasHouseGuid);
        }

        public override HouseManagementModel Pool()
        {
            //BaseModelEntity.MeteringDeviceHouses[FiasHouseGuid].Add(MeteringDevices);
            return BaseModelEntity;
        }
    }
}
