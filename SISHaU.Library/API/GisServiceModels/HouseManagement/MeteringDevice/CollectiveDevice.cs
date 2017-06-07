using Integration.HouseManagement;

namespace SISHaU.Library.API.GisServiceModels.HouseManagement.MeteringDevice
{
    public class CollectiveDevice
    {
        private MeteringDeviceBasicCharacteristicsTypeCollectiveDevice CollectiveDeviceType { get; set; }

        public CollectiveDevice()
        {
            CollectiveDeviceType = new MeteringDeviceBasicCharacteristicsTypeCollectiveDevice();
        }

        public MeteringDeviceBasicCharacteristicsTypeCollectiveDevice Build()
        {
            return CollectiveDeviceType;
        }
    }
}
