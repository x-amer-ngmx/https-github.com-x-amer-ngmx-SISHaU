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
                @"D:\test5.zip"
            };
            var tmpPath = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);



            foreach (var patch in files)
            {

                var fName = Path.GetFileNameWithoutExtension(patch);
                var res = new List<ByteDetectorModel>();
                long sizeFile = 0;
                using (var file = new FileStream(patch, FileMode.Open, FileAccess.Read, FileShare.Read))
                {

                    sizeFile = file.Length;
                    var modes = file.Length % ConstantModel.MaxPartSize;
                    var mod = modes == 0;

                    var parts = (int)(file.Length / ConstantModel.MaxPartSize) + 1;

                    

                    var part = 1;
                    long partTo = 0;

                    while (part <= parts)
                    {

                        partTo = part == 1 ? 0 : partTo + ConstantModel.MaxPartSize;

                        var to = (int)partTo;
                        var from = (int)(part != parts ? (to + ConstantModel.MaxPartSize) : file.Length);

                        var buffSize = (int)(part != parts ? ConstantModel.MaxPartSize : file.Length - to);

                        var buffer = new byte[buffSize];

                        var c = file.Read(buffer, 0, buffSize);

                        var pat = $@"{tmpPath}\{fName}.{part}.tmp";

                        using (var tmpFile = new FileStream(pat, FileMode.Create, FileAccess.Write))
                        {
                            tmpFile.Write(buffer, 0, buffer.Length);
                        }

                        res.Add(new ByteDetectorModel { Part = part, From = to, To = from - 1, Patch = pat });
                        part++;
                    }

                }

                using (var tmpFile = new FileStream($@"{tmpPath}\{fName}.zip", FileMode.Create, FileAccess.Write))
                {
                    foreach (var pat in res)
                    {
                        var buff = File.ReadAllBytes(pat.Patch);
                        //off = pat.Part == 1 ? 0 : off + buff.Length - 1;

                        tmpFile.Write(buff, 0, buff.Length);
                    }
                }
            }
        }
    }
}
