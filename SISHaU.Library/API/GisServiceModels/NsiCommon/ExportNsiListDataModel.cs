using Integration.NsiCommon;
using Microsoft.Practices.ServiceLocation;
using SISHaU.Library.API.Interfaces;
using SISHaU.Library.Util;

namespace SISHaU.Library.API.GisServiceModels.NsiCommon
{
    public class ExportNsiListDataModel : GisUtil
    {
        private readonly INsiCommonServiceCaching _service =
            ServiceLocator.Current.GetInstance<INsiCommonServiceCaching>();

        public exportNsiListResult ProcessRequest()
        {
            return _service.ProcessRequest<exportNsiListResult>(GenerateGenericType<exportNsiListRequest>());
        }
    }
}
