using System.Collections.Generic;
using ServiceStack.ServiceHost;
using SISHaU.Library.File.Model;

namespace SISHaU.ServiceModel.Types
{
    public class DefauiltParam {
        [ApiMember(Name = "Repo", Description = "Директория удалённого сервера",
ParameterType = "enum", DataType = "string", IsRequired = true)]
        public Repo repository { get; set; }
    }

    [Api("Выгрузить файлы в ГИС ЖКХ")]
    [Route("/UploadFiles", "POST")]
    public class UploadFiles : DefauiltParam, IReturn<UploadFilesResponse>
    {
        [ApiMember(Name = "files", Description = "Список файлов для выгрузки",
            ParameterType = "List<string>", DataType = "string", IsRequired = true)]
        public List<string> files { get; set; }

    }


    [Api("Выгрузить файл в ГИС ЖКХ")]
    [Route("/UploadFile", "POST")]
    public class UploadFile : DefauiltParam, IReturn<UploadFileResponse>
    {
        [ApiMember(Name = "file", Description = "Файл для выгрузки",
            ParameterType = "string", DataType = "string", IsRequired = false)]
        public string file { get; set; }
    }

    [Api("Загрузить файл из ГИС ЖКХ")]
    [Route("/DownloadFile", "GET")]
    public class DownloadFile : IReturn<DownloadFileResponse>
    {
        [ApiMember(
            Name = "download",
            ParameterType = "object",
            DataType = "DownloadModel",
            Description = "параметр для загрузки одного файла",
            IsRequired = true)]
        public DownloadModel download { get; set; }
    }


    [Api("Загрузить файлы из ГИС ЖКХ")]
    [Route("/DownloadFiles", "GET")]
    public class DownloadFiles : IReturn<DownloadFilesResponse>
    {
        [ApiMember(
            Name = "downloads",
            ParameterType = "object",
            DataType = "List<DownloadModel>",
            Description = "коллекция параметров для загрузки файлов",
            IsRequired = true)]
        public List<DownloadModel> downloads { get; set; }
    }


    public class UploadFileResponse
    {
        public UploadeResultModel Result { get; set; }
    }

    public class UploadFilesResponse
    {
        public IList<UploadeResultModel> Result { get; set; }
    }






    public class DownloadFileResponse{
        public DownloadResultModel Result { get; set; }
    }
    
    public class DownloadFilesResponse
    {
        public IList<DownloadResultModel> Result { get; set; }
    }
}
