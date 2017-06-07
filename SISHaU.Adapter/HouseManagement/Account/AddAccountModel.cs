using System.Collections.Generic;
using System.Linq;
using Integration.HouseManagement;

namespace SISHaU.Adapter.HouseManagement.Account
{
    public class AddAccountModel<T, TU> 
        : EntityGenerator<importAccountRequestAccount, AccountModel<T, TU>> where T : class
    {
        private importAccountRequestAccount AccountEntity { get; set; }
        private IList<AccountTypeAccommodation> Accommodations { get; set; }
        private AccountTypeAccommodation Accommodation { get; set; }
        private AccountTypePayerInfo PayerInfo { get; set; }

        public AddAccountModel(AccountModel<T, TU> baseModel, importAccountRequestAccount entity)
            : base(entity, baseModel)
        {
            AccountEntity = entity;
            Accommodations = new List<AccountTypeAccommodation>();
        }

        public AccomodationModel<T, TU> AddAccomodation()
        {
            Accommodation = new AccountTypeAccommodation();
            Accommodations.Add(Accommodation);
            return new AccomodationModel<T, TU>(Accommodation, this);
        }

        public PayerInfoModel<T, TU> SetPayerInfo()
        {
            PayerInfo = new AccountTypePayerInfo();
            return new PayerInfoModel<T, TU>(PayerInfo, this);
        }

        /// <summary>
        /// Выполняет сохранение вложенных структур в текущий объект ввода.
        /// Применяется оптимизация по массиву Accomodations
        /// </summary>
        /// <returns></returns>
        public override AccountModel<T, TU> Push()
        {
            AccountEntity.Accommodation = Accommodations.ToArray();
            AccountEntity.PayerInfo = PayerInfo;
            return BaseModelEntity;
        }
    }
}
