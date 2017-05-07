using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SISHaU.Library.File.Enginer;
using SISHaU.Library.File.Model;

namespace SISHaU.Library.File
{
    public class Builder
    {
        public IEnumerable<UploadeResultModel> UploadFilesList(IEnumerable<string> files, Repo repository)
        {
            var result = new List<UploadeResultModel>();

            var bild = new EnginerFileRun();

            Parallel.ForEach(files, (file, state) =>
            {
                var stream = System.IO.File.ReadAllBytes(file);
                var info = new FileInfo(file);
                var operation = new OperationFile();
                var parts = operation.ExplodingFile(stream);

                var upFile = new UploadeModel
                {
                    FileInfo = new ResultModel
                    {
                        FileName = info.Name,
                        FileSize = info.Length
                    },
                    GostHash = stream.FileGost(),
                    Parts = parts
                };

                result.Add(bild.UploadFile(upFile, repository));

            });
            



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
