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
            UploadFilesResponse result = null;
            DownloadFilesResponse downResult = null;

            using (var jsonClient = GetClient)
            {
                jsonClient.Timeout = new TimeSpan(1, 0, 0);

                try
                {

                    result = jsonClient.Post(new UploadFiles
                    {
                        FilesPathList = new List<string>()
                    {
                        @"D:\test0.zip",
                        @"D:\test1.zip",
                        @"D:\test2.zip",
                        @"D:\test3.zip",
                        @"D:\test4.zip",
                        @"D:\test5.zip",
                    },
                        RepositoryMarker = Repo.Homemanagement
                    });
                }
                catch (Exception x)
                {
                    var err = x.Message;
                }


                try
                {

                    var dmodel = new List<DownloadModel>();

                    foreach (var res in result.Result)
                    {
                        dmodel.Add(new DownloadModel
                        {
                            FileGuid = res.FileGuid,
                            Repository = Repo.Homemanagement
                        });
                    }

                    downResult = jsonClient.Get(new DownloadFiles()
                    {
                        DownloadModelList = dmodel
                    });


                    /*
                    downResult = jsonClient.Get(new DownloadFile()
                    {
                        DownloadModel = new DownloadModel
                        {
                            Repository = Repo.Homemanagement,
                            FileGuid = "4fb96f1d-7aff-4408-9b69-dc62abd49654",
                            //Parts = result.Result[0].Parts // null if [<= 5mb] or [> 5mb] new List<ByteDetectorModel>()
                        }
                    });
                    */
                }
                catch (Exception x)
                {
                    var err = x.Message;
                }

                if (downResult != null)
                    foreach (var dow in downResult.Result)
                    {
                        File.WriteAllBytes($@"D:\result\{dow.FileName}", dow.FileBytes);
                    }


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
