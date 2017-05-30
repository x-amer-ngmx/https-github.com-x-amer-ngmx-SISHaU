using System.Collections.Generic;
using System.Linq;
using Integration.HouseManagement;

namespace SISHaU.Adapter.FluentBilder.FEnginerModel.FHouseManagement.MeteringDevice
{
    public class MeteringDeviceImport : BaseModel<HouseManagementEnginer>
    {
        private importMeteringDeviceDataRequest Request { get; }
        private MeteringDeviceFullInformationType DeviceData { get; set; }
        private IList<importMeteringDeviceDataRequestMeteringDevice> MeteringDevice { get; }
        private string FiasHouseGuid { get; set; }

        public MeteringDeviceImport(HouseManagementEnginer baseModel, string fiasHouseGuid) : base(baseModel)
        {
            Request = GenerateGenericType<importMeteringDeviceDataRequest>();
            Request.FIASHouseGuid = fiasHouseGuid;
            MeteringDevice = new List<importMeteringDeviceDataRequestMeteringDevice>();
            DeviceData = new MeteringDeviceFullInformationType();
        }

        public MeteringDeviceImport CreateDevice(DeviceDataToCreate createEntity)
        {
            MeteringDevice.Add(createEntity.Build());
            return this;
        }

        public override HouseManagementEnginer Pool()
        {
            Request.MeteringDevice = MeteringDevice.ToArray();

            if (null == BaseModelEntity.MeteringDeviceImport)
                BaseModelEntity.MeteringDeviceImport = new Dictionary<string, importMeteringDeviceDataRequest>();

            if (!BaseModelEntity.MeteringDeviceImport.ContainsKey(Request.FIASHouseGuid))
                BaseModelEntity.MeteringDeviceImport.Add(Request.FIASHouseGuid, null);

            BaseModelEntity.MeteringDeviceImport[Request.FIASHouseGuid] = Request;

            return BaseModelEntity;
        }
    }
}