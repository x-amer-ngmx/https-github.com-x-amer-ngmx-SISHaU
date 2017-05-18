using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NUnit.Framework;
using SISHaU.Library.File.Model;
using SISHaU.ServiceInterface.Services;
using SISHaU.ServiceModel.Types;
using System;

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
                try { 
                var result = jsonClient.Post(new UploadFiles
                {
                    FilesPathList = new List<string>()
                    {
                        { @"D:\test.zip" }
                    },
                    RepositoryMarker = Repo.Homemanagement
                });
                }
                catch (Exception x)
                {
                    var err = x.Message;
                }
                var downResult = jsonClient.Post(new DownloadFile()
                {
                    DownloadModel = new DownloadModel
                    {
                        Repository = Repo.Homemanagement,
                        FileGuid = "GUID FILE",
                        Parts = null // null if [<= 5mb] or [> 5mb] new List<ByteDetectorModel>()
                    }
                });

                var dRes = downResult;
                //var res = result;
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
