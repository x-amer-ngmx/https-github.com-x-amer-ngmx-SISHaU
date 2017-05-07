using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SISHaU.Library.File.Model;

namespace SISHaU.Library.File
{
    public class Builder
    {
        public IEnumerable<UploadeResultModel> UploadFilesList(IEnumerable<string> files, Repo repository)
        {
            IEnumerable<UploadeResultModel> result = null;
            return result;
        }

        public UploadeResultModel UploadFiles(string files, Repo repository)
        {
            UploadeResultModel result = null;
            return result;
        }


        public IEnumerable<DownloadResultModel> DownloadFilesList(IEnumerable<DownloadModel> model)
        {
            IEnumerable<DownloadResultModel> result = null;

            return result;
        }

        public DownloadResultModel DownloadFiles(DownloadModel model)
        {
            DownloadResultModel result = null;

            return result;
        }
    }
}
