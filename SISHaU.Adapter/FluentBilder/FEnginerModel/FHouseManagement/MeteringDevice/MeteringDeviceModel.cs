namespace SISHaU.Adapter.FluentBilder.FEnginerModel.FHouseManagement.MeteringDevice
{
    public class MeteringDeviceModel : BaseModel<HouseManagementModel>
    {
        public MeteringDeviceModel(HouseManagementModel baseModel) : base(baseModel)
        {
        }

        public MeteringDeviceImport Import(string fiasHouseGuid)
        {
            return new MeteringDeviceImport(BaseModelEntity, fiasHouseGuid);
        }

        public MeteringDeviceExport Export()
        {
            return new MeteringDeviceExport(BaseModelEntity);
        }
    }
}