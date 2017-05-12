using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SISHaU.Adapter.FluentBilder.FEnginerModel.FHouseManagement
{
    public class MeteringDeviceExport : BaseModel<HouseManagementModel>
    {
        //exportMeteringDeviceDataRequest ExportParameters { get; set; }

        public MeteringDeviceExport(HouseManagementModel baseModel) : base(baseModel)
        {
            //ExportParameters = GenerateGenericType<exportMeteringDeviceDataRequest>();
        }

        public MeteringDeviceExport ByFiasGuid(string fiasGuid)
        {/*
            ExportParameters.FIASHouseGuid = fiasGuid;
            ExportParameters.FIASHouseGuidSpecified = true;
            */
            return this;
        }

        public override HouseManagementModel Pool()
        {
            //BaseModelEntity.MetringDeviceExport = ExportParameters;
            return BaseModelEntity;
        }
    }
}
