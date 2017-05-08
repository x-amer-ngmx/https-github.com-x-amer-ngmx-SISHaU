using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using SISHaU.Library.File.Model;

namespace SISHaU.Library.File.Enginer
{
    public class ResponseRequestOnServer : IDisposable
    {

        private UriRequestModel RequestUri { get; }


        public ResponseRequestOnServer(Repo rep)
        {
            RequestUri = new UriRequestModel
            {
                Repository = rep
            };
        }

        //Uploade
        // Простая загрузка PUT /ext-bus-file-store-service/rest/homemanagement/ HTTP/1.1

        // Инициализация сессии POST /ext-bus-file-store-service/rest/homemanagement/?upload HTTP/1.1
        // Загрузка части PUT /ext-bus-file-store-service/rest/homemanagement/dc9441c7-312a-4210-b77f-ea368359795f HTTP/1.1
        // Завершение сессии POST /ext-bus-file-store-service/rest/homemanagement/dc9441c7-312a-4210-b77f-ea368359795f?completed HTTP/1.1

        // Получение сведение о загружаемом файле HEAD /ext-bus-file-store-service/rest/homemanagement/dc9441c7-312a-4210-b77f-ea368359795f HTTP/1.1

        //Downloade
        // GET /ext-bus-file-store-service/rest/homemanagement/dc9441c7-312a-4210-b77f-ea368359795f?getfile HTTP/1.1

        /*
         *Тип построени адресного запроса
         * global EndPointShare
         * 
         * Repo Repository
         * HttpMethod Method
         * Uri UriRequest = {EndPointShare}{Repository}/{param}
         * 
        */



        // UriPartLoader = $"{XSrvLocation}{dir.Name()}";
        // UriStartSessionPart = $"{UriPartLoader}/?upload";
        // UriSessionID = $"{UriPartLoader}/{_sessionId}";
        // UriCloseSessionPart = $"{UriSessionID}?compleate";

        /// <summary>
        /// Метод формирует HTTP запрос на запуск сессии при передачи файла частями.
        /// </summary>
        /// <param name="fileName">Наименование файла с расширением</param>
        /// <param name="fileSize">Размер файла</param>
        /// <param name="partCount">Кол-во частей</param>
        /// <returns></returns>
        public HttpRequestMessage RequestLoadingUnitStartSession(string fileName, long fileSize, int partCount)
        {
            RequestUri.Method = HttpMethod.Post;
            RequestUri.UriRequest = "?upload";

            var result = RequestHead();
            result.Headers.Add(HeadParam.X_Upload_Filename.GetName(), fileName);
            result.Headers.Add(HeadParam.X_Upload_Length.GetName(), $"{fileSize}");
            result.Headers.Add(HeadParam.X_Upload_Part_Count.GetName(), $"{partCount}");

            return result;
        }

        /// <summary>
        /// Метод формирует HTTP запрос на завершение сессии при передачи файла частями.
        /// </summary>
        /// <param name="sessionId">Идентификатор сессии</param>
        /// <returns></returns>
        public HttpRequestMessage RequestLoadingUnitCloseSession(string sessionId)
        {
            RequestUri.Method = HttpMethod.Post;
            RequestUri.UriRequest = $"{sessionId}?completed";

            var result = RequestHead();

            return result;
        }

        /// <summary>
        /// Метод возвращает интформацию о выгружаемом (Uploade) файле
        /// </summary>
        /// <param name="fileId">Идентификатор файла, он же идентификатор сессии</param>
        /// <returns></returns>
        public HttpRequestMessage RequestLoadingUnitInfo(string fileId)
        {
            RequestUri.Method = HttpMethod.Head;
            RequestUri.UriRequest = $"{fileId}";
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
        /// <param name="sessionId">Параметр задаётся в случае загрузки частями и является идентификатором сессии</param>
        /// <returns>Возврашает http-сообщение</returns>
        public HttpRequestMessage RequestLoadingPart(byte[] part, long partSize, byte[] md5, object param, string sessionId = null)
        {
            string name;

            if (param is string)
            {
                RequestUri.UriRequest = string.Empty;
                name = HeadParam.X_Upload_Filename.GetName();
            }
            else if (param is int)
            {
                RequestUri.UriRequest = $"{sessionId}";
                name = HeadParam.X_Upload_Partnumber.GetName();
            }
            else throw new Exception("Тип передаваяемого параметра не распознан.");

            RequestUri.Method = HttpMethod.Put;

            var result = RequestHead();

            result.Headers.Add(name, $"{param}");

            result.Content = GetHttpContent(part, partSize, md5);

            return result;
        }

        /// <summary>
        /// Метод формирует HTTP запрос на загрузку файла.
        /// 
        /// Если файл больше 5мб то параметр range не задаються.
        /// </summary>
        /// <param name="fileId">Идентификатор файла, он же идентификатор сессии</param>
        /// <param name="range">Параметр задаёт диапазон размерности, загружаемого, массива байт</param>
        /// <returns></returns>
        public HttpRequestMessage RequestDownLoading(string fileId, RangeModel range)
        {
            RequestUri.Method = HttpMethod.Get;
            RequestUri.UriRequest = $"{fileId}?getfile";

            var result = RequestHead();

            if(range!=null) result.Headers.Range = new RangeHeaderValue(range.From, range.To);

            return result;
        }

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
            var result = new HttpRequestMessage(RequestUri.Method, new Uri(RequestUri.UriRequest))
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

        private void Dispose(bool disposing)
        {
            if (!disposing) return;

            RequestUri?.Dispose();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        ~ResponseRequestOnServer()
        {
            Dispose(false);
        }
    }
}
