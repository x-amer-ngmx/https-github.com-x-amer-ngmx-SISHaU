using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using SISHaU.Library.File.Model;

namespace SISHaU.Library.File.Enginer
{
    public class EnginerFileRun : IDisposable
    {
        private readonly ResponseRequestOnServer _serverConnect;
        private HttpRequestMessage _request;
        private HttpResponseMessage _response;

        private readonly Repo _repository;

        #region Выгрузка фыйлов

        public EnginerFileRun(Repo repository)
        {
            _repository = repository;
            _serverConnect = new ResponseRequestOnServer(_repository);
        }

        public UploadeResultModel UploadFile(UploadeModel uploadeMod)
        {
            if (uploadeMod ==null || !uploadeMod.Parts.Any())
            {
                return null;
            }
            UploadeResultModel result = null;

            var count = uploadeMod.Parts.Count();

            var fileGuid = string.Empty;
            IList<ByteDetectorModel> fileParts = null;
            DateTime? fileDate = null;

            RequestErrorModel error = null;

            if (count > 1)
            {
                _request = _serverConnect.RequestLoadingUnitStartSession(uploadeMod.FileInfo.FileName, uploadeMod.FileInfo.FileSize,
                    count);

                _response = _serverConnect.SendRequest(_request).Result;
                var content = _response.Content.ReadAsStringAsync().Result;
                var session = _response.ResultEnginer<ResponseIdModel>();

                //Убираю лимит на количество одновременных запросов.
                ServicePointManager.DefaultConnectionLimit = 15;

                //возможно эту часть необходимо вынести в отдельный метод для потдержки докачки частей в случае сбоя
                //и нужно рассмотреть возможность рекурсивной работы этого метода
                Parallel.ForEach(uploadeMod.Parts, (part, state) =>
                {
                    //распаралелить
                    _request = _serverConnect.RequestLoadingPart(part.Unit, part.Unit.Length, part.Md5Hash, part.PartDetect.Part, session.UploadId);
                    _response = _serverConnect.SendRequest(_request).Result;

                    var stateUploaded = _response.ResultEnginer<ResponseModel>();
                    if (stateUploaded.ServerError != null)
                    {
                        //Возникла ошибка при загрузке части
                    }

                });

                _request = _serverConnect.RequestLoadingUnitCloseSession(session.UploadId);

                _response = _serverConnect.SendRequest(_request).Result;

                var closeSess = _response.ResultEnginer<ResponseSessionCloseModel>();

                if (closeSess.IsClose == false)
                {
                    //Ошибка сессию по какой-то причине не удалось закрыть
                    error = new RequestErrorModel();
                }
                else
                {
                    fileGuid = session.UploadId;
                    fileParts = uploadeMod.Parts.Select(p => p.PartDetect).ToList();
                    fileDate = closeSess.ResultDate?.DateTime;
                }

            }
            else if (count == 1)
            {
                var part = uploadeMod.Parts.FirstOrDefault();

                //это никогда не наступит но решарпер предупреждает...
                if (part == null) return null;

                _request = _serverConnect.RequestLoadingPart(part.Unit, part.Unit.Length, part.Md5Hash,
                    uploadeMod.FileInfo.FileName);
                _response = _serverConnect.SendRequest(_request).Result;
                var uploadeId = _response.ResultEnginer<ResponseIdModel>(false);

                // Проверка на ошибку... это уже надо делать при непосредственных запросах... Ибо мой компилятор в мозгу физически ограничен, ну или я его сам ограничиваю))))
                
                fileGuid = uploadeId.UploadId;
                fileDate = uploadeId.ResultDate?.DateTime;
            }
            else throw new Exception("Что-то пошло не так, количество частей меньше одной.");


            result = error != null ? new UploadeResultModel{ ErrorMessage = error } : new UploadeResultModel
            {
                FileName = uploadeMod.FileInfo.FileName,
                FileSize = uploadeMod.FileInfo.FileSize,
                GostHash = uploadeMod.GostHash,
                Repository = _repository,
                FileGuid = fileGuid,
                Parts = fileParts,
                UTime = fileDate
            };


            return result;
        }

        #endregion

        #region Загрузка файлов

        public PrivateDownloadModel DownloadFile(string fileId)
        {
            PrivateDownloadModel result = new PrivateDownloadModel();

            _request = _serverConnect.RequestLoadingUnitInfo(fileId);
            _response = _serverConnect.SendRequest(_request).Result;

            var fileInfo = _response.ResultEnginer<ResponseInfoModel>();


            var modes = fileInfo.FileSize % ConstantModel.MaxPartSize;
            var parts = new List<RangeModel>();
            long partFromSize = 0;

            if (fileInfo.FileCompleateParts.Length > 0) { result.Parts = new List<PrivateExplodUnitModel>(); }

            foreach (var part in fileInfo.FileCompleateParts)
            {
                var thisPartSize = (partFromSize + ConstantModel.MaxPartSize);
                var partToSize = fileInfo.FileSize < thisPartSize ? fileInfo.FileSize : thisPartSize;

                var to = partToSize - 1;

                _request = _serverConnect.RequestDownLoading(fileId, new RangeModel
                {
                    From = partFromSize,
                    To = to
                });
                _response = _serverConnect.SendRequest(_request, "application/zip").Result;

                var stream = _response.Content.ReadAsByteArrayAsync().Result;



                result.Parts.Add(new PrivateExplodUnitModel {
                    
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
            _request?.Dispose();
            _response?.Dispose();

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
