using SISHaU.Library.File.Model;
using System.Collections.Generic;

namespace SISHaU.Library.File
{
    public interface IFileBuilder
    {
        IList<UploadeResultModel> UploadFilesList(IList<string> patch, Repo repository);
        UploadeResultModel UploadFiles(string patch, Repo repository);
        IList<DownloadResultModel> DownloadFilesList(IList<DownloadModel> model);
        DownloadResultModel DownloadFiles(DownloadModel model);
    }
}
