using System.Text.RegularExpressions;
using Integration.HouseManagement;

namespace SISHaU.Adapter.FluentBilder.FEnginerModel.FHouseManagement.Account
{
    public class PayerInfo
    {
        AccountTypePayerInfo PayerInfoField { get; set; }

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

        public PayerInfo FirstName(string firstname)
        {
            Create();
            PayerInfoField.Ind.FirstName = firstname;
            return this;
        }

        public PayerInfo Surname(string surname)
        {
            Create();
            PayerInfoField.Ind.Surname = surname;
            return this;
        }

        public PayerInfo Patronymic(string patronymic)
        {
            Create();
            PayerInfoField.Ind.Patronymic = patronymic;
            var rex = new Regex("$вна", RegexOptions.IgnoreCase);
            PayerInfoField.Ind.Sex = rex.IsMatch(patronymic) ? AccountIndTypeSex.F : AccountIndTypeSex.M;
            return this;
        }
    }
}
