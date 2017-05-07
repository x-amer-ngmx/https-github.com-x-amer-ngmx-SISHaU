using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using SISHaU.Library.File.Model;

namespace SISHaU.Library.File.Enginer
{
    public class EnginerFileRun : IDisposable
    {
        private readonly ResponseRequestOnServer _serverConnect;
        private HttpRequestMessage _request;
        private HttpResponseMessage _response;

        #region Выгрузка фыйлов

        public EnginerFileRun(Repo repository)
        {
            _serverConnect = new ResponseRequestOnServer(repository);
        }

        public UploadeResultModel UploadFile(UploadeModel uploadeMod, Repo repository)
        {
            UploadeResultModel result = null;

            var parts = uploadeMod.Parts as IEnumerable<ExplodUnitModel>;
            var part = uploadeMod.Parts as ExplodUnitModel;

            if (parts != null)
            {
                _request = _serverConnect.RequestLoadingUnitStartSession(uploadeMod.FileInfo.FileName, uploadeMod.FileInfo.FileSize,
                    parts.Count());

                 _response = _serverConnect.SendRequest(_request).Result;

                var sessionId = "123";
                //Убираю лимит на количество одновременных запросов.
                ServicePointManager.DefaultConnectionLimit = 15;
                Parallel.ForEach(parts, (par, state) =>
                {
                    //распаралелить
                    _request = _serverConnect.RequestLoadingPart(par.Unit, par.Unit.Length, par.Md5Hash, par.Part, sessionId);
                    _response = _serverConnect.SendRequest(_request).Result;

                });

                _request = _serverConnect.RequestLoadingUnitCloseSession(sessionId);

                _response = _serverConnect.SendRequest(_request).Result;

            }
            else if (part!=null)
            {
                _request = _serverConnect.RequestLoadingPart(part.Unit, part.Unit.Length, part.Md5Hash, uploadeMod.FileInfo.FileName);
                _response = _serverConnect.SendRequest(_request).Result;
            }

            return result;
        }

        #endregion

        #region Загрузка файлов

        public DownloadResultModel DownloadFile(string fileId)
        {
            DownloadResultModel result = null;
            _request = _serverConnect.RequestLoadingUnitInfo(fileId);
            _response = _serverConnect.SendRequest(_request).Result;
            return result;
        }

        #endregion

        private void Dispose(bool disposing)
        {
            if (!disposing) return;

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
