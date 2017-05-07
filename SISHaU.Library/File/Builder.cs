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
    public class Builder : IDisposable
    {
        OperationFile Operation => new OperationFile();

        public IEnumerable<UploadeResultModel> UploadFilesList(IEnumerable<string> patch, Repo repository)
        {
            if (patch==null || !patch.Any()) throw new Exception("Параметр {patch} не должен быть пустым"); 

            var result = new ConcurrentBag<UploadeResultModel>();

            var bild = new EnginerFileRun(repository);

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
                
                var parts = Operation.ExplodingFile(stream);


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
                result.Add(bild.UploadFile(cupFile));
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
            if(model==null || model.Any()) throw new Exception("Параметр model не должен быть пустым");

            return model.Select(DownloadFiles).ToList();
        }

        public DownloadResultModel DownloadFiles(DownloadModel model)
        {
            
            var bild = new EnginerFileRun(model.Repository);

            var file = bild.DownloadFile(model.FileGuid);

            var result=new DownloadResultModel
            {
                ErrorMessage = file.ErrorMessage,
                FileBytes = Operation.CollectFile(file.Parts),
                FileName = file.FileInfo.FileName,
                FileSize = file.FileInfo.FileSize
            };

            return result;
        }

        private void Dispose(bool disposing)
        {
            if (!disposing) return;
            Operation.Dispose();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        ~Builder()
        {
            Dispose(false);
        }
    }
}
