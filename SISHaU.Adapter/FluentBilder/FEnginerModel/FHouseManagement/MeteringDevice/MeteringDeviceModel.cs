namespace SISHaU.Adapter.FluentBilder.FEnginerModel.FHouseManagement.MeteringDevice
{
    public class MeteringDeviceModel : BaseModel<HouseManagementModel>
    {
        public MeteringDeviceModel(HouseManagementModel baseModel) : base (baseModel)
        {
        }

        public MeteringDeviceImport Create()
        {
            return new MeteringDeviceImport(BaseModelEntity);
        }

        public MeteringDeviceExport Export()
        {
            return new MeteringDeviceExport(BaseModelEntity);
        }
    }
}