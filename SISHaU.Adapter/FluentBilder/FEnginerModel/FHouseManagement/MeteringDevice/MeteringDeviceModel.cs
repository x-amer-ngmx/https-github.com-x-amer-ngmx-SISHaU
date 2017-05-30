namespace SISHaU.Adapter.FluentBilder.FEnginerModel.FHouseManagement.MeteringDevice
{
    public class MeteringDeviceModel : BaseModel<HouseManagementEnginer>
    {
        public MeteringDeviceModel(HouseManagementEnginer baseModel) : base(baseModel)
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