using System.Collections.Generic;
using Integration.HouseManagement;
using SISHaU.Library.API.GisServiceModels.HouseManagement;

namespace SISHaU.Library.API.GisServiceModels.HouseRso
{
    public class HouseRsoModel<T, TU> : BaseModel<T, TU, HouseManagementModel> where T : class
    {

        public HouseRsoModel(HouseManagementModel baseModel, IDictionary<T, TU> modelRequest)
            : base(baseModel, modelRequest){}

        public HouseRsoModel<T, TU> ByFiasHouseGuid(string fiasHouseGuid)
        {
            var request = Convert<exportHouseRequest>();
            if (request == null) return this;
            request.FIASHouseGuid = fiasHouseGuid;
            return this;
        }
    }
}
