using System.Collections.Generic;
using Integration.HouseManagement;

namespace SISHaU.Library.API.GisServiceModels.HouseManagement.MeteringDevice
{
    public class MeteringDeviceModel<T, TU> : BaseModel<T, TU, HouseManagementModel> where T : class
    {
        public MeteringDeviceModel(HouseManagementModel baseModel, IDictionary<T, TU> modelRequest) 
            : base (baseModel, modelRequest){}

        public MeteringDeviceModel<T, TU> ByFiasHouseGuid(string fiasHouseGuid)
        {
            var request = Convert<exportMeteringDeviceDataRequest>();
            if (request == null) return this;

            request.FIASHouseGuid = fiasHouseGuid;
            request.FIASHouseGuidSpecified = true;

            return this;
        }
    }
}