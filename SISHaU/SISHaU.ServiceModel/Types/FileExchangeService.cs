using System.Collections.Generic;
using ServiceStack.ServiceHost;
using SISHaU.Library.File.Model;

namespace SISHaU.ServiceModel.Types
{
    [Api("Залить файлы в ГИС ЖКХ")]
    [Route("/UploadFiles", "POST")]
    public class UploadFiles : IReturn<UploadFilesResponse>
    {
        [ApiMember(Name = "FilesPathList", Description = "Список файлов для выгрузки",
            ParameterType = "List<string>", DataType = "string", IsRequired = true)]
        public List<string> FilesPathList { get; set; }

        [ApiMember(Name = "FilesPaths", Description = "Список файлов для выгрузки. Альтернативный метод.",
            ParameterType = "string", DataType = "string", IsRequired = false)]
        public string FilesPaths { get; set; }
        public Repo RepositoryMarker { get; set; }
    }

    public class UploadFilesResponse
    {
        public IList<UploadeResultModel> Result { get; set; }
    }

    [Api("Загрузить файл из ГИС ЖКХ")]
    [Route("/DownloadFile", "GET")]
    public class DownloadFile : IReturn<DownloadFileResponse>
    {
        public DownloadModel DownloadModel { get; set; }
    }

    public class DownloadFileResponse{
        public DownloadInfoModel Result { get; set; }

    }

    [Api("Выгрузить несколько файлов подряд из ГИС ЖКХ")]
    [Route("/DownloadFiles", "GET")]
    public class DownloadFiles : IReturn<DownloadFilesResponse>
    {
        public List<DownloadModel> DownloadModelList { get; set; }
    }

    public class DownloadFilesResponse
    {
        public IList<DownloadInfoModel> Result { get; set; }
    }
}
