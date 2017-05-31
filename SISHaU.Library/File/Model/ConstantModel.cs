using System;
using System.Configuration;
using System.Net.Http.Headers;
using System.Text;

namespace SISHaU.Library.File.Model
{
    public static class ConstantModel
    {
        public static string DateproviderId => $"{ConfigurationManager.AppSettings["data-provider-id"]}";
        public static string CertificateFingerPrint => $"{ConfigurationManager.AppSettings["certificate-thumbprint"]}";

        public static string ServerShare => $"{ConfigurationManager.AppSettings["uri-host"]}/ext-bus-file-store-service/rest/";

        public static long MaxPartSize
        {
            get
            {
                var result = ConfigurationManager.AppSettings["max-part-size"] == null ? 5242880 : long.Parse(ConfigurationManager.AppSettings["max-part-size"]);

                return result <= 0 ? 1 : result;
            }
        }

        public static string TempPatch
        {
            get
            {
                var result = ConfigurationManager.AppSettings["temp-part-patch"] == null ? string.Empty : (string)ConfigurationManager.AppSettings["temp-part-patch"];
                result = string.IsNullOrEmpty(result)
                    ? throw new Exception("Путь к директории временных файлов не может быть пустым")
                    : System.IO.Directory.Exists(result) ? result 
                    : throw new Exception($"Путь \"{result}\" несушествует.");
                return result;
            }
        }


        /// <summary>
        /// WWW-Autentification Login
        /// </summary>
        public static string CredentName => ConfigurationManager.AppSettings["soapusername"];

        /// <summary>
        /// WWW-Autentification Password
        /// </summary>
        public static string CredentPass => ConfigurationManager.AppSettings["soappassword"];

        /// <summary>
        /// WWW-BasicAutentification Object
        /// </summary>
        public static AuthenticationHeaderValue XAutent => new AuthenticationHeaderValue("Basic", Convert.ToBase64String(
            Encoding.Default.GetBytes($"{CredentName}:{CredentPass}")));
    }
}
