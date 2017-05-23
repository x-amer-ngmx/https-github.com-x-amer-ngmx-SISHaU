using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using SISHaU.Library.File.Enginer;
using SISHaU.Library.File.Model;

namespace SISHaU.Library.File
{
    public interface IBuilder
    {
        IList<UploadeResultModel> UploadFilesList(IList<string> patch, Repo repository);
        UploadeResultModel UploadFiles(string patch, Repo repository);
        IList<DownloadResultModel> DownloadFilesList(IList<DownloadModel> model);
        DownloadResultModel DownloadFiles(DownloadModel model);
    }

    public class Builder : IDisposable, IBuilder
    {
        OperationFile Operation => new OperationFile();

        public IList<UploadeResultModel> UploadFilesList(IList<string> patch, Repo repository)
        {
            if (patch==null || !patch.Any()) throw new Exception($"Параметр {patch} не должен быть пустым"); 

            var result = new ConcurrentBag<UploadeResultModel>();

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

                Logger.InitLogger();//инициализация - требуется один раз в начале

                Logger.Log.Info("Ура заработало!");

                var parts = Operation.ExplodingFile(stream);

                Operation.Dispose();

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
                var upl = new EnginerFileRun(repository);
                var res = upl.UploadFile(cupFile);
                upl.Dispose();
                result.Add(res);
                Thread.Sleep(100);
            });

            return result.ToList();
        }

        public UploadeResultModel UploadFiles(string patch, Repo repository)
        {
            var result = UploadFilesList( new []{ patch } , repository).SingleOrDefault();
            return result;
        }


        public IList<DownloadResultModel> DownloadFilesList(IList<DownloadModel> model)
        {
            if (model == null || !model.Any()) throw new Exception("Параметр model не должен быть пустым");

            var collect = new ConcurrentBag<DownloadModel>(model);
            var result = new List<DownloadResultModel>();

            Parallel.ForEach(collect, (download, state) =>
            {
                result.Add(DownloadFiles(download));
                Thread.Sleep(100);
            });

            return result;
        }

        public DownloadResultModel DownloadFiles(DownloadModel model)
        {
            
            var bild = new EnginerFileRun(model.Repository);

            var file = bild.DownloadFile(model.FileGuid);

            bild.Dispose();

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
