using System;
using System.Collections.Generic;
using System.Configuration;
using System.Net;
using System.Security.Cryptography;
using System.ServiceModel;
using System.ServiceModel.Description;
using System.Xml;
using SISHaU.Library.API.ServicePointBehavior;
using SISHaU.Library.Util;
using SISHaU.Signature.Gis;

namespace SISHaU.Library.API
{
    public class GisBinder : GisUtil
    {
        private static readonly string ServicePointAddress = ConfigurationManager.AppSettings["servicepoint_" + CurrentPlatform];

        private static readonly Dictionary<string, string> ServicesEndpoints = new Dictionary<string, string>
        {
            {"BillsPortsType", "https://api.dom.gosuslugi.ru/ext-bus-bills-service/services/Bills"},
            {"BillsPortsTypeAsync", "https://api.dom.gosuslugi.ru/ext-bus-bills-service/services/BillsAsync"},
            {"DeviceMeteringPortTypes", "https://api.dom.gosuslugi.ru/ext-bus-device-metering-service/services/DeviceMetering"},
            {"DeviceMeteringPortTypesAsync", "https://api.dom.gosuslugi.ru/ext-bus-device-metering-service/services/DeviceMeteringAsync"},
            {"HouseManagementPortsType", "https://api.dom.gosuslugi.ru/ext-bus-home-management-service/services/HomeManagement"},
            {"HouseManagementPortsTypeAsync", "https://api.dom.gosuslugi.ru/ext-bus-home-management-service/services/HomeManagementAsync"},
            {"NsiCommonPortsType", "https://api.dom.gosuslugi.ru/ext-bus-nsi-common-service/services/NsiCommon"},
            {"NsiCommonPortsTypeAsync", "https://api.dom.gosuslugi.ru/ext-bus-nsi-common-service/services/NsiCommonAsync"},
            {"NsiPortsType", "https://api.dom.gosuslugi.ru/ext-bus-nsi-service/services/Nsi"},
            {"NsiPortsTypeAsync", "https://api.dom.gosuslugi.ru/ext-bus-nsi-service/services/NsiAsync"},
            {"RegOrgPortsType", "https://api.dom.gosuslugi.ru/ext-bus-org-registry-service/services/OrgRegistry"},
            {"RegOrgPortsTypeAsync", "https://api.dom.gosuslugi.ru/ext-bus-org-registry-service/services/OrgRegistryAsync"},
            {"InfrastructurePortsType", "https://api.dom.gosuslugi.ru/ext-bus-rki-service/services/Infrastructure"},
            {"InfrastructurePortsTypeAsync", "https://api.dom.gosuslugi.ru/ext-bus-rki-service/services/InfrastructureAsync"},
            {"FASPortsTypeAsync", "https://api.dom.gosuslugi.ru/ext-bus-fas-service/services/FASAsync"},
            {"PaymentPortsTypeAsync", "https://api.dom.gosuslugi.ru/ext-bus-payment-service/services/PaymentAsync"}
        };

        protected T GetProxy<T>() where T : class
        {
            var instanceType = typeof(T);
            var clientName = instanceType.Name.Replace("Client", "");
            var time = new TimeSpan(0, 0, 10, 0);

            if (!ServicesEndpoints.ContainsKey(clientName))
            {
                throw new ArgumentOutOfRangeException(
                    $@"В наборе конечных точек не обнаружено соответствие для текущего сервиса.");
            }

            var address = ServicesEndpoints[clientName].Replace("https://api.dom.gosuslugi.ru/", ServicePointAddress);
            var isHttps = 0 == ServicePointAddress.IndexOf("https://", StringComparison.Ordinal);

            var binding = new BasicHttpBinding
            {
                MaxReceivedMessageSize = int.MaxValue,
                MaxBufferSize = int.MaxValue,
                OpenTimeout = time,
                SendTimeout = time,
                ReceiveTimeout = time,
                CloseTimeout = time,
                AllowCookies = false,
                ReaderQuotas = new XmlDictionaryReaderQuotas
                {
                    MaxNameTableCharCount = int.MaxValue,
                    MaxStringContentLength = int.MaxValue,
                    MaxArrayLength = 32768,
                    MaxBytesPerRead = 4096,
                    MaxDepth = 32
                },
                Security =
                {
                    Mode = isHttps ? BasicHttpSecurityMode.Transport : BasicHttpSecurityMode.None,
                    Transport =
                    {
                        ClientCredentialType = isHttps ? HttpClientCredentialType.Certificate : HttpClientCredentialType.Basic
                    }
                }
            };

            var instance = Activator.CreateInstance(instanceType, binding, new EndpointAddress(address));

            var clientCredentials = instanceType.GetProperty("ClientCredentials");
            if (clientCredentials != null)
            {
                var clientCredentialsObject = clientCredentials.GetValue(instance) as ClientCredentials;
                if (null == clientCredentialsObject) return null;

                clientCredentialsObject.UserName.UserName = ConfigurationManager.AppSettings["basic_user"];
                clientCredentialsObject.UserName.Password = ConfigurationManager.AppSettings["basic_password"];

                if (isHttps)
                {
                    var certificate = GisSignatureHelper.FindCertificate();
                    if (certificate?.Thumbprint == null) throw new CryptographicException("Sertificate not found");
                    clientCredentialsObject.ClientCertificate.Certificate = certificate;
                    ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;
                    ServicePointManager.CheckCertificateRevocationList = false;
                    ServicePointManager.ServerCertificateValidationCallback = (sender, certificate2, chain, sslPolicyErrors) => true;
                }
            }

            ServicePointManager.Expect100Continue = false;

            var endpointType = instanceType.GetProperty("Endpoint");
            if (endpointType != null)
            {
                var endpointObject = endpointType.GetValue(instance) as ServiceEndpoint;
                endpointObject?.Behaviors.Add(new ServiceBehavior());
            }

            return instance as T;
        }
    }
}
