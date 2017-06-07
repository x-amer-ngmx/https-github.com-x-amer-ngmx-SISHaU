using Integration.HouseManagement;
using Integration.HouseManagementService;
using Integration.HouseManagementServiceAsync;

namespace SISHaU.Library.API.GisServiceModels.HouseManagement
{
    public class ExportMeteringDeviceDataModel : Requester<HouseManagementPortsTypeClient, HouseManagementPortsTypeAsyncClient>
    {
        private exportMeteringDeviceDataRequest GisRequest { get; }
        private string FiasHouseGuid { get; }

        public ExportMeteringDeviceDataModel(string fiasHouseGuid/*, string meteringDeviceRootGuid, string meteringDeviceVersionGuid*/)
        {
            FiasHouseGuid = fiasHouseGuid;
            GisRequest = GenerateGenericType<exportMeteringDeviceDataRequest>();
        }

        public exportMeteringDeviceDataRequest CreateRequest(string fiasHouseGuid)
        {
            var request = GenerateGenericType<exportMeteringDeviceDataRequest>();
            request.FIASHouseGuid = fiasHouseGuid;
            return request;
        }

        public exportMeteringDeviceDataResult ProcessRequest()
        {
            if (!string.IsNullOrEmpty(FiasHouseGuid))
            {
                GisRequest.FIASHouseGuid = FiasHouseGuid;
                //request.MeteringDeviceRootGUID = MeteringDeviceRootGuid;
                //request.MeteringDeviceVersionGUID = MeteringDeviceVersionGuid;
            }
            GisRequest.IsCurrentOrganization = true;

            return ProcessRequest<exportMeteringDeviceDataResult>(GisRequest);
        }
    }
}
