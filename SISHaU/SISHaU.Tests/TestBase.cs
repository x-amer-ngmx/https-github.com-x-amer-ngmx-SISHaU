using System;
using System.Configuration;
using System.Net;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ServiceStack.ServiceClient.Web;

namespace SISHaU.Tests
{
    [TestClass]
    public class TestBase
    {
        private static readonly string BaseUrl = ConfigurationManager.AppSettings["base_url"];
        private static TestAppHost _appHost;

        public JsonServiceClient GetClient => new JsonServiceClient
        {
            BaseUri = BaseUrl/*,
            Proxy = new WebProxy
            {
                Address = new Uri("http://localhost:3128"),
                BypassProxyOnLocal = false
            }*/
        };

        [AssemblyInitialize]
        public static void SetUp(TestContext ctx)
        {
            _appHost = InitHost();
        }

        private static TestAppHost InitHost()
        {
            var host = HostUtils.InitHost<TestAppHost>();
            host.Start(BaseUrl);
            return host;
        }

        [AssemblyCleanup]
        public static void CleanUp()
        {
            _appHost.Stop();
            _appHost.Dispose();
        }
    }
}
