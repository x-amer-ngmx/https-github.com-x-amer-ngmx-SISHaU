﻿using System.Collections.Generic;
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
    public class RestTest1
    {
        [TestMethod]
        public void TestUploadFileByRest()
        {
            UploadFilesResponse result = null;
            DownloadFilesResponse downResult = null;

            using (var jsonClient = new TestBase().GetClient)
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
                        //@"D:\test3.zip",
                        //@"D:\test4.zip",
                        //@"D:\test5.zip",
                    },
                        RepositoryMarker = Repo.Homemanagement
                    });
                }
                catch (Exception x)
                {
                    var err = x.Message;
                }
            }

            using (var jsonClient = new TestBase().GetClient)
            {
                jsonClient.Timeout = new TimeSpan(1, 0, 0);
                try
                {
                    /*
                var dmodel = new List<DownloadModel> {
                    new DownloadModel{
                        FileGuid = "d0380637-4b01-4c07-8c19-2a4c42929e1a",
                        Repository = Repo.Homemanagement
                    },
                    new DownloadModel{
                        FileGuid = "ced9dd6e-9752-45ec-ac87-55da9b0513f2",
                        Repository = Repo.Homemanagement
                    },
                    new DownloadModel{
                        FileGuid = "1554573a-c560-4f8e-a123-a7be42c85fe0",
                        Repository = Repo.Homemanagement
                    }
                };
                    */


                    var dmodel = new List<DownloadModel>();

                    foreach (var res in result.Result)
                    {
                        dmodel.Add(new DownloadModel
                        {
                            FileGuid = res.FileGuid,
                            Repository = res.Repository.Value
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
                            FileGuid = "13e56042-5159-4a18-a9ae-11add9566ee7",
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
                {

                    //File.WriteAllBytes($@"D:\result\{downResult.Result.FileName}", downResult.Result.FileBytes);


                    foreach (var dow in downResult.Result)
                    {
                        File.WriteAllBytes($@"D:\result\{dow.FileName}", dow.FileBytes);
                    }
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
