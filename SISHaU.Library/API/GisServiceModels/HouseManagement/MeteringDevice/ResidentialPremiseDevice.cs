using System.Collections.Generic;
using System.Linq;
using Integration.HouseManagement;

namespace SISHaU.Library.API.GisServiceModels.HouseManagement.MeteringDevice
{
    public class ResidentialPremiseDevice
    {
        private MeteringDeviceBasicCharacteristicsTypeResidentialPremiseDevice ResidentialPremiseDeviceType { get; }
        private IList<string> AccountGuids { get; set; }

        public  ResidentialPremiseDevice()
        {
            ResidentialPremiseDeviceType = new MeteringDeviceBasicCharacteristicsTypeResidentialPremiseDevice();
        }

        public ResidentialPremiseDevice AddAccount(string accountGuid)
        {
            if(null == AccountGuids)
                AccountGuids = new List<string>();

            AccountGuids.Add(accountGuid);

            return this;
        }

        public ResidentialPremiseDevice ResidentialPremise(string rpGuid)
        {
            ResidentialPremiseDeviceType.PremiseGUID = rpGuid;
            return this;
        }

        public MeteringDeviceBasicCharacteristicsTypeResidentialPremiseDevice Buid()
        {
            ResidentialPremiseDeviceType.AccountGUID = AccountGuids.ToArray();
            return ResidentialPremiseDeviceType;
        }
    }
}