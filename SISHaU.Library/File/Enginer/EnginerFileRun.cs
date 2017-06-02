using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using SISHaU.Library.File.Model;
using System.Net.Http;
using System.Threading;

namespace SISHaU.Library.File.Enginer
{
    //TODO: EnginerFileRun - Нужна реализация дозагрузки\довыгрузки
    public sealed class EnginerFileRun : IDisposable
    {
        private readonly ResponseRequestOnServer _serverConnect;

        private readonly Repo _repository;

        #region Выгрузка фыйлов

        public EnginerFileRun(Repo repository)
        {
            _repository = repository;
            _serverConnect = new ResponseRequestOnServer(_repository);
        }

        public UploadeResultModel UploadFile(SplitFileModel uploadeMod, ref ParallelOptions qu)
        {
            if (uploadeMod == null || !uploadeMod.Parts.Any())
            {
                return null;
            }

            UploadeResultModel result;

            var count = uploadeMod.Parts.Count;


            if (count > 1)
            {
                result = BigUploadeFile(uploadeMod.FileInfo, count, uploadeMod.Parts, ref qu);

            }
            else if (count == 1)
            {
                result = SmaillUploadeFile(uploadeMod.FileInfo, uploadeMod.Parts.FirstOrDefault());
            }
            else throw new Exception("Что-то пошло не так, количество частей меньше одной.");

            return result;
        }

        private UploadeResultModel BigUploadeFile(ResultModel fileInfo, int partCount, IList<UpPartInfoModel> parts, ref ParallelOptions quite)
        {
            ResponseIdModel session;
            ResponseSessionCloseModel sessionClose;

            //Убираю лимит на количество одновременных запросов.
            ServicePointManager.DefaultConnectionLimit = 15;

            var index = 0;
            while (true)
            {
                var response = _serverConnect.RequestLoadingUnitStartSession(
                    fileInfo.FileName,
                    fileInfo.FileSize,
                    partCount).SendRequest();

                if (index > 0) Thread.Sleep(10000 * index);
                if (index > 6)
                {
                    //Зывершаем поток
                    quite.CancellationToken.ThrowIfCancellationRequested();
                    return new UploadeResultModel
                    {
                        ErrorMessage = new RequestErrorModel
                        {
                            ErrorCode = 400,
                            ErrorInfo = "SereverTimeOut",
                            PointErrorDescript = "Сервер не доступен или соединение было разорвано"
                        }
                    };
                }
                index++;
                if (response?.StatusCode == HttpStatusCode.OK)
                {
                    session = response.ResultEnginer<ResponseIdModel>();
                    break;
                }
                response.Dispose();
            }


            Parallel.ForEach(parts, (part, state) =>
            {

                var response = UpLoadePart(part, sessId: session.UploadId);

                var stateUploaded = response.ResultEnginer<ResponseModel>();
                response.Dispose();

                if (stateUploaded.ServerError != null)
                {
                    //partRes.Add(stateUploaded);
                    //Возникла ошибка при загрузке части
                }
                else
                {

                    if (part.Patch.IndexOf(".tmpart", StringComparison.OrdinalIgnoreCase) > 0) System.IO.File.Delete(part.Patch);
                }
                GC.Collect();
                GC.WaitForPendingFinalizers();
            });

            while (true)
            {
                var response = _serverConnect.RequestLoadingUnitCloseSession(session.UploadId).SendRequest();                

                if (index > 0) Thread.Sleep(10000 * index);
                if (index < 6) index++;
                if (response?.StatusCode == HttpStatusCode.OK)
                {
                    sessionClose = response.ResultEnginer<ResponseSessionCloseModel>();
                    break;
                }
                response.Dispose();
            }



            var result = sessionClose.IsClose == false ? new UploadeResultModel
            {
                ErrorMessage = new RequestErrorModel()
            } : new UploadeResultModel
            {
                FileGuid = session.UploadId,
                FileName = fileInfo.FileName,
                FileSize = fileInfo.FileSize,
                GostHash = fileInfo.GostHash,
                Repository = _repository,
                UTime = sessionClose.ResultDate?.DateTime
            };

            return result;
        }

        private UploadeResultModel SmaillUploadeFile(ResultModel fileInfo, UpPartInfoModel part)
        {
            var response = UpLoadePart(part, fileInfo.FileName);
            var uploadeId = response.ResultEnginer<ResponseIdModel>(false);


            var result = uploadeId.ServerError != null ? new UploadeResultModel { ErrorMessage = new RequestErrorModel() }
                : new UploadeResultModel
                {
                    FileName = fileInfo.FileName,
                    FileSize = fileInfo.FileSize,
                    GostHash = fileInfo.GostHash,
                    Repository = _repository,
                    FileGuid = uploadeId.UploadId,
                    UTime = uploadeId.ResultDate?.DateTime
                };

            //обязательно мочим временный экземпляр файла
            if (part.Patch.IndexOf(".tmpart", StringComparison.OrdinalIgnoreCase) > 0) System.IO.File.Delete(part.Patch);

            GC.Collect();
            //GC.WaitForPendingFinalizers();

            return result;

        }

        private HttpResponseMessage UpLoadePart(UpPartInfoModel part, string name = null, string sessId = null)
        {
            
            var param = (!string.IsNullOrEmpty(name) ? (object) name : part.Part);

            HttpResponseMessage result;

            var index = 0;
            while (true)
            {
                using (var partStream = System.IO.File.OpenRead(part.Patch))
                {
                    result = _serverConnect.RequestLoadingPart(
                            partStream,
                            partStream.Length,
                            part.Md5Hash,
                            param,
                            sessId)
                        .SendRequest();
                }

                if (index > 0) Thread.Sleep(1000 * index);

                if (index < 30) index++;

                if (result?.StatusCode == HttpStatusCode.OK) break;
            }

            return result;
        }

        #endregion

        #region Загрузка файлов

        public PrivateDownloadModel DownloadFile(string fileId)
        {
            PrivateDownloadModel result = new PrivateDownloadModel();

            var response = _serverConnect.RequestLoadingUnitInfo(fileId).SendRequest();

            var fileInfo = response.ResultEnginer<ResponseInfoModel>();

            long partFromSize = 0;

            if (fileInfo.FileCompleateParts.Length > 0) { result.Parts = new List<PrivateExplodUnitModel>(); }

            //TODO: Ваще необходим рефакторинг и я бы подумал над тем чтобы это всё распоточить, если есть в этом смысл...
            foreach (var part in fileInfo.FileCompleateParts)
            {
                var thisPartSize = (partFromSize + Config.MaxPartSize);
                var partToSize = fileInfo.FileSize < thisPartSize ? fileInfo.FileSize : thisPartSize;

                var to = partToSize - 1;

                response = _serverConnect.RequestDownLoading(fileId, new RangeModel
                {
                    From = partFromSize,
                    To = to
                }).SendRequest();

                //TODO: Реализовать проверку ошибок ответа (BadReqest/BadResponse)
                var stream = response.Content.ReadAsByteArrayAsync().Result;


                result.Parts.Add(new PrivateExplodUnitModel
                {

                    PartDetect = new ByteDetectorModel
                    {
                        Part = part,
                        From = partFromSize,
                        To = to
                    },

                    Unit = stream

                });


                partFromSize = thisPartSize;
            }

            response.Dispose();

            result.FileInfo = new ResultModel {
                FileName = fileInfo.FileName,
                FileSize = fileInfo.FileSize
            };
            


            return result;
        }

        #endregion

        private void Dispose(bool disposing)
        {
            if (!disposing) return;
            _serverConnect?.Dispose();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        ~EnginerFileRun()
        {
            Dispose(false);
        }
    }
}
