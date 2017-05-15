using Integration.Base;

namespace SISHaU.Library.API.Interfaces
{
    public interface IGisIntegrationService<T, TU>
    {
        TS ProcessRequest<TS>(object request) where TS : class;
        TS ProcessAsyncMessageState<TS>(getStateRequest request) where TS : class;
        TS BeginProcessRequest<TS>(object request) where TS : class;
    }
}
