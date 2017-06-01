using System;
using System.Configuration;
using System.Net.Http.Headers;
using System.Text;
using System.Collections.Generic;
using System.Linq;

namespace SISHaU
{
    public static class Config
    {
        private static Dictionary<TempType, string> _tempPatchs { get; set; }
        private static string _dataProviderId { get; set; }
        private static string _certificateFingerPrint { get; set; }
        private static string _serverShare { get; set; }
        public static long _maxPartSize { get; set; }
        private static string _credentName { get; set; }
        private static string _credentPass { get; set; }
        private static AuthenticationHeaderValue _xAutent { get; set; }


        public static string DataProviderId { get { return _dataProviderId; } }

        public static string CertificateFingerPrint { get { return _certificateFingerPrint; } }

        public static string ServerShare { get { return _serverShare; } }

        public static long MaxPartSize { get { return _maxPartSize; } }

        /// <summary>
        /// WWW-Autentification Login
        /// </summary>
        public static string CredentName { get { return _credentName; } }

        /// <summary>
        /// WWW-Autentification Password
        /// </summary>
        public static string CredentPass { get { return _credentPass; } }

        /// <summary>
        /// WWW-BasicAutentification Object
        /// </summary>
        public static AuthenticationHeaderValue XAutent { get { return _xAutent; } }


        public static void InitConfig()
        {
            var tempPatch = ConfigurationManager.AppSettings["temp-patch"];
            var uriHost = ConfigurationManager.AppSettings["uri-host"];

            _dataProviderId = ConfigurationManager.AppSettings["data-provider-id"];
            _certificateFingerPrint = ConfigurationManager.AppSettings["certificate-thumbprint"];
            _credentName = ConfigurationManager.AppSettings["soapusername"];
            _credentPass = ConfigurationManager.AppSettings["soappassword"];


            List<string> errorInfo = new List<string>();

            if (string.IsNullOrEmpty(tempPatch))
            {
                errorInfo.Add(
                    "Название параметра: 'temp-patch'\n" +
                    "Причина исключения: Путь к директории временных файлов не может быть пустым\n"+
                    new string('-',20)+"\n"
                    );

            }

            if (string.IsNullOrEmpty(_dataProviderId))
            {
                errorInfo.Add(
                    "Название параметра: 'data-provider-id'\n" +
                    "Причина исключения: Идентификатор информационной системы должен быть указан\n" +
                    new string('-', 20) + "\n"
                    );
            }

            if (string.IsNullOrEmpty(_certificateFingerPrint))
            {
                errorInfo.Add(
                    "Название параметра: 'certificate-thumbprint'\n" +
                    "Причина исключения: Отпечаток сертификата безопасности должен быть указан\n" +
                    new string('-', 20) + "\n"
                    );
            }

            if (string.IsNullOrEmpty(uriHost))
            {
                errorInfo.Add(
                    "Название параметра: 'uri-host'\n" +
                    "Причина исключения: Http адрес к серверу взаимодействия должен быть указан\n" +
                    new string('-', 20) + "\n"
                    );
            }

            if (string.IsNullOrEmpty(_credentName))
            {
                errorInfo.Add(
                    "Название параметра: 'soapusername'\n" +
                    "Причина исключения: Логин для соединения должен быть указан\n" +
                    new string('-', 20) + "\n"
                    );
            }

            if (string.IsNullOrEmpty(_credentPass))
            {
                errorInfo.Add(
                    "Название параметра: 'soappassword'\n" +
                    "Причина исключения: Пароль для соединения должен быть указан\n" +
                    new string('-', 20) + "\n"
                    );
            }


            if (!System.IO.Directory.Exists(tempPatch))
            {
                errorInfo.Add(
                    "Название параметра: 'temp-patch'\n" +
                    $"Причина исключения: Путь \"{tempPatch}\" для временных директорий несушествует\n" +
                    new string('-', 20) + "\n"
                    );
            }

            if (errorInfo.Any())
            {
                var message = new StringBuilder();

                foreach (var error in errorInfo)
                {
                    message.Append(error);
                }

                var mess = message.ToString();
                throw new Exception(mess);
            }

             
            _serverShare = $"{uriHost}/ext-bus-file-store-service/rest/";

            _xAutent = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(Encoding.Default.GetBytes($"{_credentName}:{_credentPass}")));

            _maxPartSize = ConfigurationManager.AppSettings["max-part-size"] == null ? 5242880 : long.Parse(ConfigurationManager.AppSettings["max-part-size"]);
            _maxPartSize = _maxPartSize <= 0 ? 1 : _maxPartSize;

            _tempPatchs = new Dictionary<TempType, string> {
                { TempType.Up  , $@"{tempPatch}\Upload"},
                { TempType.Down, $@"{tempPatch}\Download"},
            };

            foreach (var d in _tempPatchs)
            {
                System.IO.Directory.CreateDirectory(d.Value);
            }

        }

        public static string TempPath (TempType tmp)
        {
            return _tempPatchs[tmp];
        }

        public enum TempType {
            Up,
            Down
        }



    }
}
