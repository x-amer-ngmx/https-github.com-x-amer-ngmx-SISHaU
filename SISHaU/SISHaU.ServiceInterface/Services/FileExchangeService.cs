using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ServiceStack.ServiceHost;
using SISHaU.ServiceModel.Types;

namespace SISHaU.ServiceInterface.Services
{
    public class FileExchangeService : IService
    {
        public object Get(DownloadFiles filesInfo)
        {
            return new DownloadFilesResponse();
        }

        public object Get(DownloadFile fileInfo)
        {
            return new DownloadFileResponse();
        }

        public object Post(UploadFiles filesInfo)
        {
            return new UploadFilesResponse();
        }
    }
}
