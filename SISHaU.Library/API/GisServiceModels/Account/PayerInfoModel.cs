using System.Text.RegularExpressions;
using Integration.HouseManagement;

namespace SISHaU.Library.API.GisServiceModels.Account
{
    public class PayerInfoModel<T, TU>
            : EntityGenerator<AccountTypePayerInfo, AddAccountModel<T, TU>> where T : class
    {
        public PayerInfoModel(AccountTypePayerInfo entity, AddAccountModel<T, TU> baseModel) : base(entity, baseModel){}

        public PayerInfoModel<T, TU> AddPayerInfo(string compositeString)
        {
            var sName = compositeString.Replace(".", "").Split(';');
            var Ind = new AccountIndType{
                    Surname = string.IsNullOrEmpty(sName[1]) ? "Нет данных" : sName[0],
                    FirstName = string.IsNullOrEmpty(sName[0]) ? "Нет данных" : sName[1],
                    Patronymic = string.IsNullOrEmpty(sName[2]) ? "Нет данных" : sName[2]
            };

            var rex = new Regex("$вна", RegexOptions.IgnoreCase);
            Ind.Sex = rex.IsMatch(Ind.Patronymic) ? AccountIndTypeSex.F : AccountIndTypeSex.M;
            Set(pi => pi.Ind, Ind);
            return this;
        }
    }
}
