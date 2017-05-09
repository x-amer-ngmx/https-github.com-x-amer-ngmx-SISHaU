using System;
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

            if (count > 1)
            {
                _request = _serverConnect.RequestLoadingUnitStartSession(uploadeMod.FileInfo.FileName, uploadeMod.FileInfo.FileSize,
                    count);

                _response = _serverConnect.SendRequest(_request).Result;
                var content = _response.Content.ReadAsStringAsync().Result;
                var session = _response.ResultEnginer<ResponseIdModel>();

                //Убираю лимит на количество одновременных запросов.
                ServicePointManager.DefaultConnectionLimit = 15;
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
                }
                else 
                    result = new UploadeResultModel
                    {
                        FileName = uploadeMod.FileInfo.FileName,
                        FileSize = uploadeMod.FileInfo.FileSize,
                        GostHash = uploadeMod.GostHash,
                        Repository = _repository,
                        FileGuid = session.UploadId,
                        UTime = closeSess.ResultDate?.DateTime
                    };

            }
            else if (count == 1)
            {
                var part = uploadeMod.Parts.FirstOrDefault();

                //это никогда не наступит но решарпер предупреждает...
                if (part == null) return null;

                _request = _serverConnect.RequestLoadingPart(part.Unit, part.Unit.Length, part.Md5Hash, uploadeMod.FileInfo.FileName);
                _response = _serverConnect.SendRequest(_request).Result;
                var uploadeId = _response.ResultEnginer<ResponseIdModel>(false);

                result = new UploadeResultModel
                {
                    FileName = uploadeMod.FileInfo.FileName,
                    FileSize = uploadeMod.FileInfo.FileSize,
                    GostHash = uploadeMod.GostHash,
                    Repository = _repository,
                    FileGuid = uploadeId.UploadId,
                    UTime = uploadeId.ResultDate?.DateTime
                };
            }

            return result;
        }

        #endregion

        #region Загрузка файлов

        public UploadeModel DownloadFile(string fileId)
        {
            UploadeModel result = null;

            _request = _serverConnect.RequestLoadingUnitInfo(fileId);
            _response = _serverConnect.SendRequest(_request).Result;

            RangeModel range = null;

            _request = _serverConnect.RequestDownLoading(fileId, range);
            _response = _serverConnect.SendRequest(_request).Result;



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
