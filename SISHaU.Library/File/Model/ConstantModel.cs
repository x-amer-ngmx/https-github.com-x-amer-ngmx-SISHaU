using System;
using System.Configuration;
using System.Net.Http.Headers;
using System.Text;
using NHibernate.Hql.Ast.ANTLR;

namespace SISHaU.Library.File.Model
{
    public static class ConstantModel
    {
        public static string DataProviderId => $"{ConfigurationManager.AppSettings["data-provider-id"]}";
        public static string CertificateFingerPrint => $"{ConfigurationManager.AppSettings["certificate-thumbprint"]}";

        public static string ServerShare => $"{ConfigurationManager.AppSettings["uri-host"]}/ext-bus-file-store-service/rest/";

        public static long MaxPartSize = ConfigurationManager.AppSettings["max-part-size"] == null
            ? 5242880
            : long.Parse(ConfigurationManager.AppSettings["max-part-size"]);

        public static string TempPath 
        {
            get
            {
                var result = ConfigurationManager.AppSettings["temp-part-patch"] ?? ConfigurationManager.AppSettings["temp-part-patch"];

                if (string.IsNullOrEmpty(result))
                {
                    throw new InvalidPathException("Путь к директории временных файлов не может быть пустым");
                }

                if (!System.IO.Directory.Exists(result))
                {
                    throw new InvalidPathException($"Путь \"{result}\" несушествует.");
                }

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
