using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using SISHaU.Library.File.Model;

namespace SISHaU.Library.File.Enginer
{
    public class EnginerFileRun : IDisposable
    {

        #region Выгрузка фыйлов

        public UploadeResultModel UploadFile(UploadeModel uploadeMod, Repo repository)
        {
            UploadeResultModel result = null;
            HttpRequestMessage request;
            HttpResponseMessage response;

            var serverConnect = new ResponseRequestOnServer(repository);

            var parts = uploadeMod.Parts as IEnumerable<ExplodUnitModel>;
            var part = uploadeMod.Parts as ExplodUnitModel;

            if (parts != null)
            {
                request = serverConnect.RequestLoadingUnitStartSession(uploadeMod.FileInfo.FileName, uploadeMod.FileInfo.FileSize,
                    parts.Count());

                 response = serverConnect.SendRequest(request).Result;

                Parallel.ForEach(parts, (par, state) =>
                {
                    //распаралелить
                    request = serverConnect.RequestLoadingPart(par.Unit, par.Unit.Length, par.Md5Hash, par.Part);
                    response = serverConnect.SendRequest(request).Result;
                });

                request = serverConnect.RequestLoadingUnitCloseSession();

                response = serverConnect.SendRequest(request).Result;

            }
            else if (part!=null)
            {
                request = serverConnect.RequestLoadingPart(part.Unit, part.Unit.Length, part.Md5Hash, part.Part);
                response = serverConnect.SendRequest(request).Result;
            }

            return result;
        }

        #endregion

        #region Загрузка файлов

        public DownloadResultModel DownloadFile()
        {
            DownloadResultModel result = null;
            return result;
        }

        #endregion

        private void Dispose(bool disposing)
        {
            if (!disposing) return;
            //if (_objects != null) _objects.Dispose();
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
