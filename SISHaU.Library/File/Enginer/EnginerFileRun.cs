using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using SISHaU.Library.File.Model;
using System.Net.Http;

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

        public UploadeResultModel UploadFile(SplitFileModel uploadeMod)
        {
            if (uploadeMod == null || !uploadeMod.Parts.Any())
            {
                return null;
            }

            UploadeResultModel result;

            var count = uploadeMod.Parts.Count;


            if (count > 1)
            {
                result = BigUploadeFile(uploadeMod.FileInfo, count, uploadeMod.Parts);

            }
            else if (count == 1)
            {
                result = SmaillUploadeFile(uploadeMod.FileInfo, uploadeMod.Parts.FirstOrDefault());
            }
            else throw new Exception("Что-то пошло не так, количество частей меньше одной.");

            return result;
        }

        private UploadeResultModel BigUploadeFile(ResultModel fileInfo, int partCount, IList<ByteDetectorModel> parts)
        {
            HttpResponseMessage response;

            var index = 0;
            while (true)
            {
                response = _serverConnect.RequestLoadingUnitStartSession(
                    fileInfo.FileName,
                    fileInfo.FileSize,
                    partCount).SendRequest();

                if (index > 0) System.Threading.Thread.Sleep(10000 * index);
                if(index < 6) index++;
                if(response.StatusCode == HttpStatusCode.OK) break;
            }
            

            //var content = response.Content.ReadAsStringAsync().Result;
            var session = response.ResultEnginer<ResponseIdModel>();

            //Убираю лимит на количество одновременных запросов.
            ServicePointManager.DefaultConnectionLimit = 15;

            //var partRes = new List<ResponseModel>();

            Parallel.ForEach(parts, (part, state) =>
            {

                response = UpLoadePart(part, sessId: session.UploadId);

                var stateUploaded = response.ResultEnginer<ResponseModel>();

                if (stateUploaded.ServerError != null)
                {
                    //partRes.Add(stateUploaded);
                    //Возникла ошибка при загрузке части
                }
                else
                {
                    
                    if (part.Patch.IndexOf(".tmpart", StringComparison.OrdinalIgnoreCase) > 0) System.IO.File.Delete(part.Patch);
                }

            });

            while (true)
            {
                response = _serverConnect.RequestLoadingUnitCloseSession(session.UploadId).SendRequest();

                if (index > 0) System.Threading.Thread.Sleep(10000 * index);
                if (index < 6) index++;
                if (response.StatusCode == HttpStatusCode.OK) break;
            }
            
            var closeSess = response.ResultEnginer<ResponseSessionCloseModel>();
            
                //Ошибка сессию по какой-то причине не удалось закрыть

                var result = closeSess.IsClose == false ? new UploadeResultModel
                {
                    ErrorMessage = new RequestErrorModel()
                } : new UploadeResultModel
                {
                    FileGuid = session.UploadId,
                    FileName = fileInfo.FileName,
                    FileSize = fileInfo.FileSize,
                    Parts = parts,
                    GostHash = fileInfo.GostHash,
                    Repository = _repository,
                    UTime = closeSess.ResultDate?.DateTime
                };
            
            response.Dispose();

            return result;
        }

        private UploadeResultModel SmaillUploadeFile(ResultModel fileInfo, ByteDetectorModel part)
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
                    Parts = new[] { part },
                    UTime = uploadeId.ResultDate?.DateTime
                };

            return result;

        }

        private HttpResponseMessage UpLoadePart(ByteDetectorModel part, string name = null, string sessId = null)
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

                if (index > 0) System.Threading.Thread.Sleep(1000 * index);

                if(index < 30) index++;

                if (result.StatusCode == HttpStatusCode.OK) break;
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
                var thisPartSize = (partFromSize + ConstantModel.MaxPartSize);
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
