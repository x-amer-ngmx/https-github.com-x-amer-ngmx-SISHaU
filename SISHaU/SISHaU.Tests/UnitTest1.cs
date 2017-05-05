using System;
using NUnit.Framework;
using ServiceStack;
using ServiceStack.ServiceInterface.Testing;
using SISHaU.ServiceModel;
using SISHaU.ServiceInterface;

namespace SISHaU.Tests
{
    [TestFixture]
    public class UnitTests
    {
        //private readonly BasicAppHost appHost;

        public UnitTests()
        {
            /*ar appHost = new BasicAppHost(typeof(MyServices).Assembly)
            {

            }
            .Init();*/
        }

        [OneTimeTearDown]
        public void TestFixtureTearDown()
        {
            //appHost.Dispose();
        }

        [Test]
        public void Test_Method1()
        {
            //var service = appHost.Container.Resolve<MyServices>();
            /*
            var response = (HelloResponse)service.Any(new Hello { Name = "World" });

            Assert.That(response.Result, Is.EqualTo("Hello, World!"));*/
        }
    }
}
