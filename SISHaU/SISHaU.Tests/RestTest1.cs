using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NUnit.Framework;
using SISHaU.Library.File.Model;
using SISHaU.ServiceInterface.Services;
using SISHaU.ServiceModel.Types;

namespace SISHaU.Tests
{
    [TestClass]
    public class RestTest1 : TestBase
    {
        [TestMethod]
        public void TestUploadFileByRest()
        {
            using (var jsonClient = GetClient)
            {
                var result = jsonClient.Post(new UploadFiles
                {
                    FilesPathList = new List<string>()
                    {
                        { @"D:\gens_214ru.zip" }
                    },
                    RepositoryMarker = Repo.Agreements
                });

                var res = result;
            }
        }

        [TestMethod]
        public void TestUploadFileByService()
        {
            var service = new FileExchangeService();

            var result = service.Post(new UploadFiles
            {
                FilesPathList = new List<string>()
                {
                    {@"c:\Orchard.Source.zip"}
                },
                RepositoryMarker = Repo.Agreements
            });
        }
    }
}
