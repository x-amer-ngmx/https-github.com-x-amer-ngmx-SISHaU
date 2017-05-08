using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
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

        public static T ResultEnginer<T>(this HttpResponseMessage respons) where T : class
        {
            var result = Activator.CreateInstance(typeof(T));


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
                { typeof(ResponseIdModel), () =>
                {
                    result = new ResponseIdModel { };
                }},
                { typeof(ResponseSessionCloseModel), () =>
                {
                    result = new ResponseSessionCloseModel { };
                }},
                { typeof(ResponseInfoModel), () =>
                {
                    result = new ResponseInfoModel { };
                }},
                { typeof(ResponseDownloadModel), () =>
                {
                    result = new ResponseDownloadModel { };
                }},
            };

            typeSwitcher[typeof(T)]();

            return (T) result;
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
    }
}
