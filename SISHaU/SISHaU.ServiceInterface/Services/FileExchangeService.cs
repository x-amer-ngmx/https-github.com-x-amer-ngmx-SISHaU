using Microsoft.Practices.ServiceLocation;
using ServiceStack.ServiceHost;
using SISHaU.Library.File;
using SISHaU.ServiceModel.Types;

namespace SISHaU.ServiceInterface.Services
{
    public class FileExchangeService : IService
    {
        private readonly IBuilder _fileExchange = ServiceLocator.Current.GetInstance<IBuilder>();

        public DownloadFilesResponse Get(DownloadFiles filesInfo)
        {
            return new DownloadFilesResponse
            {
                Result = _fileExchange.DownloadFilesList(filesInfo.DownloadModelList)
            };
        }

        public DownloadFileResponse Get(DownloadFile fileInfo)
        {
            return new DownloadFileResponse
            {
                Result = _fileExchange.DownloadFiles(fileInfo.DownloadModel)
            };
        }

        public UploadFilesResponse Post(UploadFiles filesInfo)
        {
            return new UploadFilesResponse
            {
                Result = _fileExchange.UploadFilesList(filesInfo.FilesPathList, filesInfo.RepositoryMarker)
            };
        }
    }
}
