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
        public IEnumerable<UploadeResultModel> UpLoaderFiles(IEnumerable<string> files, Repo repository)
        {
            IEnumerable<UploadeResultModel> result = null;
            return result;
        }

        public UploadeResultModel UpLoaderFiles(string files, Repo repository)
        {
            UploadeResultModel result = null;
            return result;
        }


        public IEnumerable<DownloadeResultModel> DownLoaderFiles(IEnumerable<DownloadeModel> model)
        {
            IEnumerable<DownloadeResultModel> result = null;

            return result;
        }

        public DownloadeResultModel DownLoaderFiles(DownloadeModel model)
        {
            DownloadeResultModel result = null;

            return result;
        }
    }
}
