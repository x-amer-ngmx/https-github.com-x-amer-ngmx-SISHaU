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
        public DownloadFilesResponse Get(DownloadFiles filesInfo)
        {
            return new DownloadFilesResponse();
        }

        public DownloadFileResponse Get(DownloadFile fileInfo)
        {
            return new DownloadFileResponse();
        }

        public UploadFilesResponse Post(UploadFiles filesInfo)
        {

            return new UploadFilesResponse();
        }
    }
}
