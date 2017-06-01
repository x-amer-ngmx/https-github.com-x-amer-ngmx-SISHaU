using System;
using System.Configuration;
using System.IO;
using System.Net.Http.Headers;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Dispatcher;
using System.Text;
using System.Xml;
using SISHaU.Library.File.Model;
using SISHaU.Signature.Gis;

namespace SISHaU.Library.API.ServicePointBehavior
{
    public class ServiceClientInspector : IClientMessageInspector
    {
        private static readonly string BasicUser = ConfigurationManager.AppSettings["basic_user"];
        private static readonly string BasicPassword = ConfigurationManager.AppSettings["basic_password"];
        private static readonly string SignId = ConfigurationManager.AppSettings["sign_id"];
        private static readonly string BaseUrl = ConfigurationManager.AppSettings["base_url"];

        private readonly bool IsLogging = ConfigurationManager.AppSettings["has_logging"] != null &&
                                          Boolean.Parse(ConfigurationManager.AppSettings["has_logging"]);

        private static readonly string SignerUrl = ConfigurationManager.AppSettings["signer_url_iis"];

        private static readonly bool RemoteSigner = ConfigurationManager.AppSettings["remote_signer"] != null &&
                                                    bool.Parse(ConfigurationManager.AppSettings["remote_signer"]);

        static AuthenticationHeaderValue GetAuthHeaderValue()
        {
            var auth = Encoding.Default.GetBytes(BasicUser + ":" + BasicPassword);
            var authI = Convert.ToBase64String(auth);
            return new AuthenticationHeaderValue("Basic", authI);
        }

        private void LogRequestAsXml(string xmlData)
        {
            if (xmlData.Contains("getStateRequest") || !BaseUrl.Contains("localhost") || !IsLogging) return;
            var xmlFile = new XmlDocument();
            xmlFile.LoadXml(xmlData);

            using (var xmlWritter = new XmlTextWriter(@"LastRequest.xml", Encoding.UTF8))
            {
                xmlFile.WriteTo(xmlWritter);
                xmlWritter.Flush();
                xmlWritter.Close();
            }
        }

        public object BeforeSendRequest(ref Message request, IClientChannel channel)
        {
            var messageRef = MessageString(ref request);

            LogRequestAsXml(messageRef);

            var signedXml = messageRef.Contains(SignId) ? SignXmlData(messageRef) : messageRef;

            request = CreateMessageFromString(signedXml, request.Version);

            var httpRequestMessage = new HttpRequestMessageProperty();

            //Если нужен отпечаток

            httpRequestMessage.Headers.Add("X-Client-Cert-Fingerprint", Config.CertificateFingerPrint.Replace(" ", ""));
            httpRequestMessage.Headers.Add("Authorization", GetAuthHeaderValue().ToString());

            request.Properties.Add(HttpRequestMessageProperty.Name, httpRequestMessage);

            return null;
        }

        private string SignXmlData(string messageRef)
        {
            //ToDo потом надо подумать, нужен ли REST сервис для подписи
            //Todo также подумать об осинхронной реализации
            return GisSignatureHelper.SignXml(messageRef);

            //if (!RemoteSigner)
            //    return GisSignatureHelper.SignXml(messageRef);

            //using (var jsonClient = new JsonServiceClient
            //{
            //    BaseUri = SignerUrl
            //    /*Proxy = new WebProxy
            //    {
            //        Address = new Uri("http://localhost:3128"),
            //        BypassProxyOnLocal = false
            //    }*/
            //})
            //{
            //    var signedFile = jsonClient.Post(new SignXml { XmlData = Util.Util.Zip(messageRef) });
            //    return Util.Util.Unzip(signedFile.Result);
            //}
        }

        public void AfterReceiveReply(ref Message reply, object correlationState)
        {
            //Thread.Sleep(1);
        }

        /// <summary>
        /// Здесь формируется само SOAP сообщение - его можно поглядеть
        /// </summary>
        /// <param name="m"></param>
        /// <returns></returns>
        static String MessageString(ref Message m)
        {
            var mb = m.CreateBufferedCopy(int.MaxValue);
            m = mb.CreateMessage();
            var s = new MemoryStream();
            var xw = XmlWriter.Create(s);
            m.WriteMessage(xw);
            xw.Flush();
            s.Position = 0;

            var bXml = new byte[s.Length];
            s.Read(bXml, 0, (int) s.Length);

            return bXml[0] != (byte) '<'
                ? Encoding.UTF8.GetString(bXml, 3, bXml.Length - 3)
                : Encoding.UTF8.GetString(bXml, 0, bXml.Length);
        }

        static XmlReader XmlReaderFromString(String xml)
        {
            var stream = new MemoryStream();
            var writer = new StreamWriter(stream);
            writer.Write(xml);
            writer.Flush();
            stream.Position = 0;
            return XmlReader.Create(stream);
        }

        static Message CreateMessageFromString(string xml, MessageVersion ver)
        {
            return Message.CreateMessage(XmlReaderFromString(xml), int.MaxValue, ver);
        }
    }
}
