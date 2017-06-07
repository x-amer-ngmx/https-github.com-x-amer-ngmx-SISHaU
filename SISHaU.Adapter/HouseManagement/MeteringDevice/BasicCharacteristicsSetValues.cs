using System;

namespace SISHaU.Adapter.HouseManagement.MeteringDevice
{
    public class BasicCharacteristicsSetValues
    {
        public string MeteringDeviceNumber { get; set; }
        public string MeteringDeviceModel { get; set; }
        public string MeteringDeviceStamp { get; set; }
        public DateTime InstallationDate { get; set; }
        public DateTime CommissioningDate { get; set; }
        public DateTime FirstVerificationDate { get; set; }
    }
}