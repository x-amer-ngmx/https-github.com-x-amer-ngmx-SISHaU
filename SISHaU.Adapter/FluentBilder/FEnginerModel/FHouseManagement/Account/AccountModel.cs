using System;
using Integration.HouseManagement;

namespace SISHaU.Adapter.FluentBilder.FEnginerModel.FHouseManagement.Account
{
    public class AccountModel : BaseModel<HouseManagementEnginer>
    {
        private importAccountRequestAccount Account { get; }

        public AccountModel(HouseManagementEnginer baseModel) : base(baseModel)
        {
            Account = new importAccountRequestAccount
            {
                TransportGUID = Guid.NewGuid().ToString(),
                isRSOAccount = true
            };
        }

        public AccountModel WithCreationDate(DateTime creationDate)
        {
            Account.CreationDate = creationDate;
            return this;
        }

        public AccountModel WithAccountReason(AccountReason accountReason)
        {
            Account.AccountReasons = accountReason.Create();
            return this;
        }

        public AccountModel WithPayerInfo(PayerInfo payerInfo)
        {
            Account.PayerInfo = payerInfo.Create();
            return this;
        }

        public AccountModel HeatedArea(decimal heatedArea)
        {
            Account.HeatedArea = heatedArea;
            return this;
        }

        public AccountModel TotalSquareArea(decimal totalSquare)
        {
            Account.TotalSquare = totalSquare;
            return this;
        }

        public AccountModel ResidentialSquare(decimal residentialSquare)
        {
            Account.TotalSquare = residentialSquare;
            return this;
        }

        public AccountModel LivingPersonsNumber(sbyte prersonsCount)
        {
            Account.LivingPersonsNumber = prersonsCount;
            return this;
        }

        public AccountModel AccountNumber(string number)
        {
            Account.AccountNumber = number;

            return this;
        }

        public AccountModel Update(string accountGuid)
        {
            Account.AccountGUID = accountGuid;
            return this;
        }

        public PayerInfo PayerInfo()
        {
            return new PayerInfo();
        }

        public override HouseManagementEnginer Pool()
        {
            BaseModelEntity.Accounts.Add(Account);
            return BaseModelEntity;
        }
    }
}
