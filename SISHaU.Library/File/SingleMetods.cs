using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Cryptography;
using CryptoPro.Sharpei;
using SISHaU.Library.File.Model;

namespace SISHaU.Library.File
{
    public static class SingleMetods
    {
        public static string GetName(this Enum en)
        {
            var name = Enum.GetName(en.GetType(), en);
            var result = name?.Replace('_', '-');
            return en is Repo ? result?.ToLower() : result;
        }

        public static bool ToBool(this string res)
        {
            return bool.Parse(res);
        }

        public static int[] ToParts(this string parts, char sp = ',')
        {
            int[] result;
            try
            {
                result = parts.Split(sp).Select(int.Parse).OrderBy(x => x).ToArray();
            }
            catch (Exception)
            {
                result = null;
                //throw;
            }
            return result;
        }

        public static string GetVal(this HttpHeaders head, string param)
        {
            IEnumerable<string> str;
            var result = head.TryGetValues(param, out str) ? str.FirstOrDefault() : string.Empty;
            return result;
        }

        //Возможно это лишнее
        public static T ResponseConverter<T>(this HttpResponseMessage respons)
        {
            var a = respons.StatusCode;
            var b = respons.Headers.Date;
            var c = respons.Headers.Location; //получить UploadeID
            var d = respons.Headers.ConnectionClose;

            var e = respons.Content.Headers.ContentLength;
            var f = respons.Content.Headers.ContentType?.MediaType;
            var g = respons.Content.ReadAsByteArrayAsync().Result; // byt[]
            var h = respons.Content.ReadAsStringAsync().Result; // get error if respons.StatusCode == HttpStatusCode.BadRequest
            var j = respons.Content.Headers.LastModified;

            var x = respons.Headers.GetVal(HeadParam.X_Upload_UploadID.GetName());
            var x1 = respons.Headers.GetVal(HeadParam.X_Upload_Filename.GetName());
            var x2 = respons.Headers.GetVal(HeadParam.X_Upload_Length.GetName());
            var x3 = respons.Headers.GetVal(HeadParam.X_Upload_Completed_Parts.GetName());
            var x4 = respons.Headers.GetVal(HeadParam.X_Upload_Completed.GetName());
            var x5 = respons.Headers.GetVal(HeadParam.X_Upload_FileGUID.GetName());


            /* Вынисти анализатор HttpResponse для большей наглядности и меньшего написания повторного кода
 * Date -
 * Location -
 * X-Upload-UploadID
 * Connection -
 * X-Upload-Filename
 * X-Upload-Length
 * X-Upload-Completed-Parts
 * X-Upload-Completed
 * Last-Modified -
 * Content-Length -
 * Content-Type -
 * X-Upload-FileGUID
 * Content - X-Upload-Error / byte[]
 */
            object result = null;

            return (T)result;
        }

        public static T ResultEnginer<T>(this HttpResponseMessage respons, bool isSession = true) where T : class
        {
            T result = null;
            XErrorContext? error = null;

            /* Вынисти анализатор HttpResponse для большей наглядности и меньшего написания повторного кода
             * Date
             * Location
             * X-Upload-UploadID
             * Connection
             * X-Upload-Filename
             * X-Upload-Length
             * X-Upload-Completed-Parts
             * X-Upload-Completed
             * Last-Modified
             * Content-Length
             * Content-Type
             * X-Upload-FileGUID
             * Content - X-Upload-Error / byte[]
             * ------------------------------------
             * 2.3.6.1	Простая загрузка.
                    HTTP/1.1 200 OK
                     +Location: /ext-bus-file-store-service/rest/homemanagement/dc9441c7-312a-4210-b77f-ea368359795f
                     +Date: Mon, 1 Nov 2010 20:34:56 GMT


                    2.3.6.2	Загрузка частями.
                    2.3.6.2.1	Инициализация сессии.
                    HTTP/1.1 200 OK
                     +Date: Mon, 1 Nov 2010 20:34:56 GMT
                     +X-Upload-UploadID: dc9441c7-312a-4210-b77f-ea368359795f



                    2.3.6.2.2	Загрузка части файла.
                    HTTP/1.1 200 OK
                     +Date: Mon, 1 Nov 2010 20:34:56 GMT


                    2.3.6.2.3	Завершение сессии загрузки.
                    HTTP/1.1 200 OK
                     +Date: Mon, 1 Nov 2010 20:34:56 GMT
                     +Connection: close


                    2.3.6.2.4	Получение сведений о загружаемом файле.
                    HTTP/1.1 200 OK
                     +Date: Mon, 1 Nov 2010 20:34:56 GMT
                     +X-Upload-Filename: dogovo.tif
                     +X-Upload-Length: 20000000
                     +X-Upload-Completed-Parts: 1,2,3
                     +X-Upload-Completed: false


                    2.3.6.3	Выгрузка файла.
                    HTTP/1.1 200 OK
                     +Date: Wed, 06 Jun 2012 20:48:15 GMT
                     +Last-Modified: Wed, 06 Jun 2012 13:39:25 GMT
                     +Content-Length: 611892
                     +Content-Type: text/plain
                     +X-Upload-FileGUID: dc9441c7-312a-4210-b77f-ea368359795f
                     +[1000 bytes of object data]
             */

            //разобрать HttpResponseMessage

            var typeSwitcher = new Dictionary<Type, Action>
            {
                { typeof(ResponseModel), () =>
                {
                    result = new ResponseIdModel
                    {
                        ResultDate = respons.Headers.Date,
                        ServerError = error
                    } as T;
                }},
                { typeof(ResponseIdModel), () =>
                {

                    var id = respons.StatusCode == HttpStatusCode.OK ?  isSession
                        ? respons.Headers.GetVal(HeadParam.X_Upload_UploadID.GetName())
                        : respons.Headers.Location.ToString().Split('/').LastOrDefault()
                        : null;

                    result = new ResponseIdModel
                    {
                        ResultDate = respons.Headers.Date,
                        UploadId = id,
                        ServerError = error
                    } as T;
                }},
                { typeof(ResponseSessionCloseModel), () =>
                {
                    result = new ResponseSessionCloseModel
                    {
                        ResultDate = respons.Headers.Date,
                        IsClose = respons.StatusCode == HttpStatusCode.OK ? respons.Headers.ConnectionClose : null,
                        ServerError = error
                    } as T;
                }},
                { typeof(ResponseInfoModel), () =>
                {
                    long size = 0;

                    int[] parts = null;
                    bool? compleat = false;

                    if (respons.StatusCode == HttpStatusCode.OK)
                    {
                        long.TryParse(respons.Headers.GetVal(HeadParam.X_Upload_Length.GetName()), out size);

                        parts = respons.Headers.GetVal(HeadParam.X_Upload_Completed_Parts.GetName()).ToParts();
                        compleat = respons.Headers.GetVal(HeadParam.X_Upload_Completed.GetName()).ToBool();
                    }

                    result = new ResponseInfoModel
                    {
                        ResultDate = respons.Headers.Date,
                        FileName = respons.StatusCode == HttpStatusCode.OK ?  respons.Headers.GetVal(HeadParam.X_Upload_Filename.GetName()) : null,
                        FileSize = size,
                        FileCompleateParts = parts,
                        IsCompleate = compleat,
                        ServerError = error
                    } as T;
                }},
                { typeof(ResponseDownloadModel), () =>
                {
                    result = new ResponseDownloadModel
                    {
                        ResultDate = respons.Headers.Date,
                        FileLastModification = respons.StatusCode == HttpStatusCode.OK ? respons.Content.Headers.LastModified : null,
                        FileType = respons.StatusCode == HttpStatusCode.OK ? respons.Content.Headers.ContentType?.MediaType : null,
                        RFileBytes = respons.StatusCode == HttpStatusCode.OK ? respons.Content.ReadAsByteArrayAsync().Result : null,
                        ServerError = error
                    } as T;
                }},
            };

            typeSwitcher[typeof(T)]();

            return result;
        }

        public static byte[] FileMd5(this byte[] stream)
        {
            return MD5.Create().ComputeHash(stream);
        }

        public static string FileGost(this byte[] stream)
        {
            string result;
            var hash = new Gost3411CryptoServiceProvider().ComputeHash(stream);
            result = BitConverter.ToString(hash).Replace("-", string.Empty).ToLower();

            return result;
        }

        /// <summary>
        /// Метод отправляет http-запрос асинхронно в отдельном потоке
        /// </summary>
        /// <param name="message"></param>
        /// <returns>Асинхронную операция</returns>
        public static HttpResponseMessage SendRequest(this HttpRequestMessage message)
        {
            HttpResponseMessage result = null;

            try
            {
                string proxyUri ="http://127.0.0.1:8888";

                HttpClientHandler httpClientHandler = new HttpClientHandler()
                {
                    Proxy = new WebProxy(proxyUri, false),
                    UseProxy = true,
                    UseDefaultCredentials = false
                };

                var client = new HttpClient(httpClientHandler);

                client.Timeout = TimeSpan.FromMinutes(15);

                var sender = client.SendAsync(
                    message,
                    HttpCompletionOption.ResponseContentRead,
                    new System.Threading.CancellationTokenSource(TimeSpan.FromMinutes(15)).Token
                    );

                sender.Wait();

                result = sender.Result;
            }
            catch (Exception ex)
            {
                var resMess = ex.Message;
                //
            }
            message.Dispose();
            return result;
        }
    }
}
