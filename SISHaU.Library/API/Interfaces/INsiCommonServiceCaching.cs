namespace SISHaU.Library.API.Interfaces
{
    public interface INsiCommonServiceCaching
    {
        TS ProcessRequest<TS>(object request) where TS : class;
    }
}
