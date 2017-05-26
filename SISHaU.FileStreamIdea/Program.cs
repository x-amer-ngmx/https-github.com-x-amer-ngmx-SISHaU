using SISHaU.Library.File.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SISHaU.FileStreamIdea
{
    class Program
    {
        static void Main(string[] args)
        {
            var files = new[] {
                @"D:\test0.zip",
                @"D:\test1.zip",
                @"D:\test2.zip",
                @"D:\test3.zip",
                @"D:\test4.zip",
                @"D:\test5.zip",
                @"D:\test6.zip"
            };
            var tmpPath = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);



            foreach (var patch in files)
            {

                var res = SplitFiles(tmpPath, patch);


                using (var tmpFile = new FileStream($@"{tmpPath}\{Path.GetFileName(patch)}", FileMode.Create, FileAccess.Write))
                {
                    foreach (var pat in res)
                    {
                        var buff = File.ReadAllBytes(pat.Patch);

                        tmpFile.Write(buff, 0, buff.Length);

                        if (pat.Patch.IndexOf(".tmp") > 0) File.Delete(pat.Patch);
                    }
                }
            }
        }

        private static IList<ByteDetectorModel> SplitFiles(string tmpPath, string patch)
        {
            var result = new List<ByteDetectorModel>();
            var fName = Path.GetFileNameWithoutExtension(patch);

            using (var file = new FileStream(patch, FileMode.Open, FileAccess.Read, FileShare.Read))
            {

                var parts = (int)(file.Length / ConstantModel.MaxPartSize) + 1;

                if (parts == 1)
                {
                    result = new List<ByteDetectorModel> {
                        new ByteDetectorModel{
                            From = 0,
                            To = file.Length,
                            Part = 1,
                            Patch = patch
                        }
                    };
                    file.Dispose();
                    return result;
                }

                var part = 1;
                long partTo = 0;

                while (part <= parts)
                {
                    partTo = part == 1 ? 0 : partTo + ConstantModel.MaxPartSize;

                    long from;
                    var buffSize = 0;

                    if (part != parts)
                    {
                        from = partTo + ConstantModel.MaxPartSize;
                        buffSize = (int)(ConstantModel.MaxPartSize);
                    }
                    else
                    {
                        from = file.Length;
                        buffSize = (int)(file.Length - partTo);
                    }

                    var buffer = new byte[buffSize];
                    var partSize = file.Read(buffer, 0, buffSize);

                    var pat = $@"{tmpPath}\{file.Length}_{fName}.{part}.tmp";

                    using (var tmpFile = new FileStream(pat, FileMode.Create, FileAccess.Write))
                    {
                        tmpFile.Write(buffer, 0, partSize);
                    }

                    result.Add(new ByteDetectorModel { Part = part, From = partTo, To = from - 1, Patch = pat });
                    part++;
                }

            }

            return result;
        }
    }
}
