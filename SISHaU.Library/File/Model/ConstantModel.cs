using System;
using System.Configuration;
using System.Net.Http.Headers;
using System.Text;

namespace SISHaU.Library.File.Model
{
    public static class ConstantModel
    {
        public static string DateproviderId => $"{ConfigurationManager.AppSettings["data-provider-id"]}";

        public static string ServerShare => $"{ConfigurationManager.AppSettings["uri_host"]}/ext-bus-file-store-service/rest/";
        public static long MaxPartSize => ConfigurationManager.AppSettings["max-part-size"]==null ? 5242880 :
                                          long.Parse(ConfigurationManager.AppSettings["max-part-size"]);

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
