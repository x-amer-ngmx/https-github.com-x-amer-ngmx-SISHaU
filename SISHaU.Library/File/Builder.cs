using System;
using System.Collections.Concurrent;
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
        public IEnumerable<UploadeResultModel> UploadFilesList(IEnumerable<string> patch, Repo repository)
        {
            if (patch==null || !patch.Any()) throw new Exception("Параметр {patch} не должен быть пустым"); 

            var result = new ConcurrentBag<UploadeResultModel>();

            var bild = new EnginerFileRun();

            var upFile = new List<UploadeModel>();

            foreach (var file in patch)
            {
                if (!System.IO.File.Exists(file))
                {
                    result.Add(new UploadeResultModel
                    {
                        ErrorMessage = new RequestErrorModel
                        {
                            ErrorCode = 404,
                            ErrorInfo = $"По указанному пути {{{file}}} файл не был обнаружен.",
                            PointErrorDescript = $"Предупреждение произошло в {{UploadFilesList}}"
                        }
                    });
                    continue;
                }

                var stream = System.IO.File.ReadAllBytes(file);
                var info = new FileInfo(file);
                var operation = new OperationFile();
                var parts = operation.ExplodingFile(stream);


                upFile.Add(new UploadeModel
                {
                    FileInfo = new ResultModel
                    {
                        FileName = info.Name,
                        FileSize = info.Length
                    },
                    GostHash = stream.FileGost(),
                    Parts = parts
                });

            }

            Parallel.ForEach(upFile, (cupFile, state) =>
            {
                result.Add(bild.UploadFile(cupFile, repository));
            });

            return result;
        }

        public UploadeResultModel UploadFiles(string patch, Repo repository)
        {
            var result = UploadFilesList( new []{ patch } , repository).SingleOrDefault();

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
