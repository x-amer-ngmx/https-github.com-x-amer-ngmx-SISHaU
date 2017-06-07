using Integration.HouseManagement;

namespace SISHaU.Library.API.GisServiceModels.Account
{
    public class AccomodationModel<T, TU> 
        : EntityGenerator<AccountTypeAccommodation, AddAccountModel<T, TU>> where T : class
    {
        public AccomodationModel(AccountTypeAccommodation entity, AddAccountModel<T, TU> baseModel) : base(entity, baseModel){}
    }
}
