using NUnit.Framework;
using SISHaU.ServiceModel.Types;

namespace SISHaU.Tests
{
    [TestFixture]
    public class RestTest1 : TestBase
    {
        [Test]
        public void TestUploadFile()
        {
            using (var jsonClient = GetClient)
            {
                var result = jsonClient.Post(new UploadFiles
                {
                    
                });
            }
        }
    }
}
