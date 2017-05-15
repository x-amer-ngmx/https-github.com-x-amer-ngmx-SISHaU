using System.Collections.Generic;
using System.Linq;
using Integration.HouseManagement;

namespace SISHaU.Adapter.FluentBilder.FEnginerModel.FHouseManagement.MeteringDevice
{
    public class MeteringDeviceImport : BaseModel<HouseManagementModel>
    {
        private importMeteringDeviceDataRequest Request { get; set; }
        private MeteringDeviceFullInformationType DeviceData { get; set; }
        private IList<importMeteringDeviceDataRequestMeteringDevice> MeteringDevice { get; set; }
        public MeteringDeviceImport(HouseManagementModel baseModel) : base(baseModel)
        {
            Request = GenerateGenericType<importMeteringDeviceDataRequest>();
            MeteringDevice = new List<importMeteringDeviceDataRequestMeteringDevice>();
            DeviceData = new MeteringDeviceFullInformationType();
        }

        public MeteringDeviceImport AddDevice()
        {
            MeteringDevice.Add(new importMeteringDeviceDataRequestMeteringDevice());
            return this;
        }

        public override HouseManagementModel Pool()
        {
            Request.MeteringDevice = MeteringDevice.ToArray();
            if (null == BaseModelEntity.MeteringDeviceImport)
                BaseModelEntity.MeteringDeviceImport = new List<importMeteringDeviceDataRequest>();

            return BaseModelEntity;
        }
    }
}