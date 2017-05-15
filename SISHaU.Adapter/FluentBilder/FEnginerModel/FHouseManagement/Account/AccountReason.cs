using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Integration.HouseManagement;

namespace SISHaU.Adapter.FluentBilder.FEnginerModel.FHouseManagement.Account
{
    public class AccountReason
    {
        AccountReasonsImportType AccountReasonType { get; set; }

        public AccountReasonsImportType Create()
        {
            if (null == AccountReasonType)
                AccountReasonType = new AccountReasonsImportType();

            if (null == AccountReasonType.SupplyResourceContract)
                AccountReasonType.SupplyResourceContract = new AccountReasonsImportTypeSupplyResourceContract();

            return AccountReasonType;
        }

        public AccountReason ContractNumber(string contractNumber)
        {
            Create();
            AccountReasonType.SupplyResourceContract.ContractNumber = contractNumber;
            return this;
        }

        public AccountReason SigningDate(string signingDate)
        {
            Create();
            AccountReasonType.SupplyResourceContract.ContractNumber = signingDate;
            return this;
        }
    }
}
