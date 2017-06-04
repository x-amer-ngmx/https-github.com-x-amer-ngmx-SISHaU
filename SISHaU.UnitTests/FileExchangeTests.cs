using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SISHaU.Library.File;
using SISHaU.ServiceModel.Types;
using SISHaU.Library.File.Model;
using System.Linq;

namespace SISHaU.UnitTests
{
    [TestClass]
    public class FileExchangeTests
    {
        [TestMethod]
        public void TestUploadFiles()
        {
            
            Config.InitConfig();

            /*System.IO.DirectoryInfo dir = new System.IO.DirectoryInfo(@"D:\TestFileUploade");

            var files = dir.EnumerateFiles().Select(x=> $@"{x.FullName}").ToList();*/

            var fileServiceBuilder = new Builder();
            var doUpload = new UploadFiles
            {
                
                FilesPathList = /*files*/ new List<string>
                {
                        @"D:\test0.zip",
                        @"D:\test1.zip",
                        @"D:\test2.zip",
                        @"D:\test3.zip",
                        @"D:\test4.zip",
                        @"D:\test5.zip",
                        @"D:\test6.zip",
                        @"D:\test7.zip",
                        /*@"D:\test8.zip",
                        @"D:\test9.zip",
                        @"D:\test10.zip",
                        @"D:\test11.zip",
                        @"D:\test12.zip",
                        @"D:\test13.zip",
                        @"D:\test14.zip"*/
                },
                RepositoryMarker = Repo.Homemanagement
            };
            var result = fileServiceBuilder.UploadFilesList(doUpload.FilesPathList, doUpload.RepositoryMarker);
            /*
            System.Threading.Thread.Sleep(15000);

            var down = result.Select(x => new DownloadModel { FileGuid = x.FileGuid, Repository = x.Repository.Value }).ToList();

            var downResult = fileServiceBuilder.DownloadFilesList(down);

            if (downResult != null)
            {
                foreach (var dow in downResult)
                {
                    File.WriteAllBytes($@"D:\result\{dow.FileName}", dow.FileBytes);
                }
            }*/

        }
    }
}
