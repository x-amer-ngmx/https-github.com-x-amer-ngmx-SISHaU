using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NUnit.Framework;
using SISHaU.Library.File.Model;
using SISHaU.ServiceInterface.Services;
using SISHaU.ServiceModel.Types;
using System;
using System.IO;

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
                UploadFilesResponse result = null;
                DownloadFileResponse downResult = null;
                try {
                    /*
                result = jsonClient.Post(new UploadFiles
                {
                    FilesPathList = new List<string>()
                    {
                        { @"D:\test.zip" }
                    },
                    RepositoryMarker = Repo.Homemanagement
                });
                    */
                downResult = jsonClient.Get(new DownloadFile()
                {
                    DownloadModel = new DownloadModel
                    {
                        Repository = Repo.Homemanagement,
                        FileGuid = "4fb96f1d-7aff-4408-9b69-dc62abd49654",
                        //Parts = result.Result[0].Parts // null if [<= 5mb] or [> 5mb] new List<ByteDetectorModel>()
                    }
                });
                }
                catch (Exception x)
                {
                    var err = x.Message;
                }

                if(downResult!=null)
                File.WriteAllBytes($@"D:\result\{downResult.Result.FileName}", downResult.Result.FileBytes);

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
