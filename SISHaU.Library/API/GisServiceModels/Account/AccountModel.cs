using System.Collections.Generic;
using Integration.HouseManagement;
using SISHaU.Library.API.GisServiceModels.HouseManagement;
using System.Linq;

namespace SISHaU.Library.API.GisServiceModels.Account
{
    public class AccountModel<T, TU> : BaseModel<T, TU, HouseManagementModel> where T : class
    {
        private importAccountRequestAccount Account { get; set; }
        private IList<importAccountRequestAccount> Accounts { get; set; }

        public AccountModel(HouseManagementModel baseModel, IDictionary<T, TU> modelRequest)
            : base(baseModel, modelRequest)
        {
            Accounts = new List<importAccountRequestAccount>();
        }

        public AddAccountModel<T, TU> AddAccount()
        {
            Account = new importAccountRequestAccount();
            Accounts.Add(Account);
            return new AddAccountModel<T, TU>(this, Account);
        }

        public AccountModel<T, TU> ByFiasHouseGuid(string fiasHouseGuid)
        {
            var request = Convert<exportAccountRequest>();
            if (request == null) return this;
            request.FIASHouseGuid = fiasHouseGuid;
            return this;
        }

        /*public AccountModel<T, TU> WithCreationDate(DateTime creationDate)
        {
            Account.CreationDate = creationDate;
            return this;
        }

        public AccountModel<T, TU> WithAccountReason(AccountReason accountReason)
        {
            Account.AccountReasons = accountReason.Create();
            return this;
        }

        public AccountModel<T, TU> WithPayerInfo(PayerInfo<T, TU> payerInfo)
        {
            Account.PayerInfo = payerInfo.Create();
            return this;
        }

        public AccountModel<T, TU> HeatedArea(decimal heatedArea)
        {
            Account.HeatedArea = heatedArea;
            return this;
        }

        public AccountModel<T, TU> TotalSquareArea(decimal totalSquare)
        {
            Account.TotalSquare = totalSquare;
            return this;
        }

        public AccountModel<T, TU> ResidentialSquare(decimal residentialSquare)
        {
            Account.TotalSquare = residentialSquare;
            return this;
        }

        public AccountModel<T, TU> LivingPersonsNumber(sbyte prersonsCount)
        {
            Account.LivingPersonsNumber = prersonsCount;
            return this;
        }

        public AccountModel<T, TU> AccountNumber(string number)
        {
            Account.AccountNumber = number;

            return this;
        }

        public AccountModel<T, TU> Update(string accountGuid)
        {
            Account.AccountGUID = accountGuid;
            return this;
        }

        public AccountModel<T, TU> Accomodation(string premiseGuid)
        {
            Account.Accommodation = new[] {
                new AccountTypeAccommodation{
                    LivingRoomGUID = premiseGuid,
                    LivingRoomGUIDSpecified = true
            }};

            return this;
        }

        public PayerInfo<T, TU> PayerInfo()
        {
            return new PayerInfo<T, TU>(this);
        }*/

        public override HouseManagementModel Push()
        {
            var request = Convert<importAccountRequest>();
            if (request == null) return BaseModelEntity;
            request.Account = Accounts.ToArray();
            return BaseModelEntity;
        }
    }
    /*
    public class PayerInfo<T, TU> where T : class
    {
        private AccountTypePayerInfo PayerInfoField { get; set; }
        private AccountModel<T, TU> BaseModel { get; }

        public PayerInfo(AccountModel<T, TU> baseModel)
        {
            BaseModel = baseModel;
        }

        public AccountModel<T, TU> ToAccount()
        {
            var privateAccountFieldInfo = typeof(AccountModel<T,TU>).GetProperty("Account",
                BindingFlags.NonPublic | BindingFlags.GetField | BindingFlags.Instance);
            var privateAccountField = (importAccountRequestAccount)privateAccountFieldInfo.GetValue(BaseModel);
            privateAccountField.PayerInfo = PayerInfoField;
            return BaseModel;
        }

        public AccountTypePayerInfo Get()
        {
            return PayerInfoField;
        }

        public AccountTypePayerInfo Create()
        {
            if (null != PayerInfoField) return PayerInfoField;

            PayerInfoField = new AccountTypePayerInfo
            {
                Ind = new AccountIndType
                {
                    FirstName = "Нет данных",
                    Surname = "Нет данных",
                    Patronymic = "Нет данных"
                }
            };

            return PayerInfoField;
        }

        public PayerInfo<T, TU> FirstName(string firstname)
        {
            Create();
            PayerInfoField.Ind.FirstName = firstname;
            return this;
        }

        public PayerInfo<T, TU> Surname(string surname)
        {
            Create();
            PayerInfoField.Ind.Surname = surname;
            return this;
        }

        public PayerInfo<T, TU> Patronymic(string patronymic)
        {
            Create();
            PayerInfoField.Ind.Patronymic = patronymic;
            var rex = new Regex("$вна", RegexOptions.IgnoreCase);
            PayerInfoField.Ind.Sex = rex.IsMatch(patronymic) ? AccountIndTypeSex.F : AccountIndTypeSex.M;
            return this;
        }
    }

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
    }*/
}
