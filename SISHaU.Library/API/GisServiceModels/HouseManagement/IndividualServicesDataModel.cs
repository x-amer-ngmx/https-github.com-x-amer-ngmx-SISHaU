using System.Collections.Generic;
using Integration.HouseManagement;
using Integration.HouseManagementService;
using Integration.HouseManagementServiceAsync;
using Integration.NsiBase;
using SISHaU.Library.API;

namespace ServiceHelperHost.Library.GisServiceModel.HouseManagement
{
    public class IndividualServicesDataModel : Requester<HouseManagementPortsTypeClient, HouseManagementPortsTypeAsyncClient>
    {
        private NsiCommonDataModel NsiModel { get; }
        public Dictionary<string, nsiRef> CachedServices { get; set; }

        public IndividualServicesDataModel()
        {
            NsiModel = new NsiCommonDataModel();
            CachedServices = new Dictionary<string, nsiRef>();
        }

        public void ImportIndividualServiceForAccount(string accountGuid, string serviceName)
        {

            if (!CachedServices.ContainsKey(serviceName)){
                CachedServices.Add(serviceName, NsiModel.GetAdditionalService(serviceName));
            }

            var request = GenerateGenericType<importAccountIndividualServicesRequest>();

            request.IndividualService = new importAccountIndividualServicesRequestIndividualService{
                TransportGUID = TransportGuid,
                AccountGUID = accountGuid,
                AdditionalService = CachedServices[serviceName]
            };

            request.IndividualServiceSpecified = true;

            ProcessRequest<ImportResult>(request);
        }

        /// <summary>
        /// Услуги не могут повторяться
        /// </summary>
        /// <param name="serviceName"></param>
        public void AddServiceNameCaching(string serviceName)
        {
            if (CachedServices.ContainsKey(serviceName)) return;
            CachedServices.Add(serviceName, NsiModel.GetAdditionalService(serviceName));
        }
    }
}
