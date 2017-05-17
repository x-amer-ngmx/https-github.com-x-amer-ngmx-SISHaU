using System.Collections.Generic;
using Integration.HouseManagement;

namespace SISHaU.Adapter.FluentBilder.FEnginerModel.FHouseManagement.MeteringDevice
{
    public class MeteringDeviceExport : BaseModel<HouseManagementEnginer>
    {
        private exportMeteringDeviceDataRequest ExportParameters { get; set; }

        public MeteringDeviceExport(HouseManagementEnginer baseModel) : base(baseModel)
        {
            ExportParameters = GenerateGenericType<exportMeteringDeviceDataRequest>();
        }

        public MeteringDeviceExport ByFiasGuid(string fiasGuid)
        {
            ExportParameters.FIASHouseGuid = fiasGuid;
            ExportParameters.FIASHouseGuidSpecified = true;
            return this;
        }

        public override HouseManagementEnginer Pool()
        {
            if (null == BaseModelEntity.MeteringDeviceExport)
                BaseModelEntity.MeteringDeviceExport = new List<exportMeteringDeviceDataRequest>();

            BaseModelEntity.MeteringDeviceExport.Add(ExportParameters);

            return BaseModelEntity;
        }
    }
}