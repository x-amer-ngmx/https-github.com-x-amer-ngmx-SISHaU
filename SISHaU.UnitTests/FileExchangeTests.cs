using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SISHaU.Library.File;
using SISHaU.ServiceModel.Types;
using SISHaU.Library.File.Model;

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
                    @"C:\GIT\Test\test0.zip",
                    @"C:\GIT\Test\test1.zip",
                    @"C:\GIT\Test\test2.zip"
                },
                RepositoryMarker = Repo.Homemanagement
            };
            var result = fileServiceBuilder.UploadFilesList(doUpload.FilesPathList, doUpload.RepositoryMarker);
        }
    }
}
