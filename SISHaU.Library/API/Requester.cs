using Integration.Base;
using Microsoft.Practices.ServiceLocation;
using SISHaU.Library.API.Interfaces;
using SISHaU.Library.Util;

namespace SISHaU.Library.API
{
    public class Requester<T, TU> : GisUtil where T : class where TU : class
    {
        public string SessionGuid;
        private IGisIntegrationService<T, TU> GisIntegrationService { get; }
        private NsiCommonDataModel NsiModel { get; }
        public NsiCommonDataModel Nsi => NsiModel;

        public Requester()
        {
            GisIntegrationService = ServiceLocator.Current.GetInstance<IGisIntegrationService<T, TU>>();
            NsiModel = new NsiCommonDataModel();
            SessionGuid = TransportGuid;
        }

        public TS ProcessRequest<TS>(object request) where TS : class
        {
            var result = GisIntegrationService.ProcessRequest<TS>(request);
            Log(result, false);
            return result;
        }

        public getStateRequest BeginProcessRequest(object request)
        {
            return GisIntegrationService.BeginProcessRequest<getStateRequest>(request);
        }

        public TS ProcessAsyncMessageState<TS>(getStateRequest request) where TS : class
        {
            var result = GisIntegrationService.ProcessAsyncMessageState<TS>(request);
            Log(result, false);
            return result;
        }
    }
}
