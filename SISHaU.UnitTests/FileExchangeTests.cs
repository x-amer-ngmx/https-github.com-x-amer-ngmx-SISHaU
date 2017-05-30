using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SISHaU.Library.File;
using SISHaU.ServiceModel.Types;
using SISHaU.Library.File.Model;
using System.Linq;
using System.IO;

namespace SISHaU.UnitTests
{
    [TestClass]
    public class FileExchangeTests
    {
        [TestMethod]
        public void TestUploadFiles()
        {
            var fileServiceBuilder = new Builder();
            var doUpload = new UploadFiles
            {
                FilesPathList = new List<string>
                {
                        @"D:\test0.zip",
                        @"D:\test1.zip",
                        @"D:\test2.zip",
                        @"D:\test3.zip",
                        @"D:\test4.zip",
                        @"D:\test5.zip",
                        @"D:\test7.zip",
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
