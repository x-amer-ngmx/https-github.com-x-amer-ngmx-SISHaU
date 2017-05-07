using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using SISHaU.Library.File.Model;

namespace SISHaU.Library.File.Enginer
{
    public class ResponseRequestOnServer
    {

        private Uri RequestUri { get; set; }
        private Uri UriPartLoader { get; set; }

        public ResponseRequestOnServer(Repo rep)
        {
            UriPartLoader = new Uri($"{ConstantModel.ServerShare}{rep.GetName()}");
        }

        // UriPartLoader = $"{XSrvLocation}{dir.Name()}";
        // UriStartSessionPart = $"{UriPartLoader}/?upload";
        // UriSessionID = $"{UriPartLoader}/{_sessionId}";
        // UriCloseSessionPart = $"{UriSessionID}?compleate";

        public HttpRequestMessage RequestLoadingUnitStartSession(string fileName, long fileSize, int partCount)
        {
            RequestUri = new Uri($"{UriPartLoader}/?upload");

            var result = RequestHead();
            result.Headers.Add(HeadParam.X_Upload_Filename.GetName(), fileName);
            result.Headers.Add(HeadParam.X_Upload_Length.GetName(), $"{fileSize}");
            result.Headers.Add(HeadParam.X_Upload_Part_Count.GetName(), $"{partCount}");

            return result;
        }
        public HttpRequestMessage RequestLoadingUnitCloseSession()
        {
            var result = RequestHead();

            return result;
        }

        public HttpRequestMessage RequestLoadingUnitInfo()
        {
            var result = RequestHead();
            return result;
        }

        /// <summary>
        /// Общий метод формирования заголовка и тела запроса.
        /// </summary>
        /// <param name="part">Массив байт, либо файла, либо его части</param>
        /// <param name="partSize">Размер, либо файла, либо его части </param>
        /// <param name="md5">Хеш сумма файла</param>
        /// <param name="param">Принимает либо string FileName, либо int PartNumber</param>
        /// <returns>Возврашает http-сообщение</returns>
        public HttpRequestMessage RequestLoadingPart(byte[] part, long partSize, byte[] md5, object param)
        {
            var result = RequestHead();
            var name = string.Empty;

            if(param is string) name = HeadParam.X_Upload_Filename.GetName();
            else if(param is int) name = HeadParam.X_Upload_Partnumber.GetName();
            else throw new Exception("Тип передаваяемого параметра не распознан.");

            result.Headers.Add(name, $"{param}");

            result.Content = GetHttpContent(part, partSize, md5);

            return result;
        }

        /*
        public HttpRequestMessage RequestEasyLoading(byte[] part, long partSize, byte[] md5, string fileName)
        {
            var result = RequestHead();
            result.Headers.Add(HeadParam.X_Upload_Filename.GetName(), fileName);

            result.Content = GetHttpContent(part, partSize, md5);

            return result;
        }

        public HttpRequestMessage RequestLoadingUnit(byte[] part, long partSize, byte[] md5, int partNum)
        {
            var result = RequestHead();
            result.Headers.Add(HeadParam.X_Upload_Partnumber.GetName(), $"{partNum}");

            result.Content = GetHttpContent(part, partSize, md5);

            return result;
        }
        */

        /// <summary>
        /// Формируем http-content, передаём массив байт файла или части
        /// </summary>
        /// <param name="part">массив байтов части или файла</param>
        /// <param name="partSize">размер части или файла</param>
        /// <param name="md5">хэш сумма части или файла</param>
        /// <returns>Массив байт http-content</returns>
        private ByteArrayContent GetHttpContent(byte[] part, long partSize, byte[] md5)
        {
            var result = new ByteArrayContent(part);
            result.Headers.ContentLength = partSize;
            result.Headers.ContentMD5 = md5;
 
            return result;
        }

        /// <summary>
        /// Общий метод формирования заголовка
        /// </summary>
        /// <returns>Везвращает готовый общий заголовок</returns>
        private HttpRequestMessage RequestHead()
        {
            var result = new HttpRequestMessage(HttpMethod.Put, RequestUri)
            {
                Version = HttpVersion.Version11,
                Headers =
                {
                    Date = DateTimeOffset.Now,
                    Authorization = ConstantModel.XAutent
                }
            };

            result.Headers.Add(HeadParam.X_Upload_Dataprovider.GetName(), ConstantModel.DateproviderId);

            return result;
        }

        /// <summary>
        /// Метод отправляет http-запрос асинхронно в отдельном потоке
        /// </summary>
        /// <param name="message"></param>
        /// <returns>Асинхронную операция</returns>
        public Task<HttpResponseMessage> SendRequest(HttpRequestMessage message)
        {
            Task<HttpResponseMessage> result = null;

            try
            {
                result = new HttpClient().SendAsync(message, HttpCompletionOption.ResponseContentRead);
            }
            catch (Exception)
            {
                //var resMess = ex.Message;
                //
            }

            return result;
        }
    }
}
