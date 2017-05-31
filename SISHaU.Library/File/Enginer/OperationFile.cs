using System;
using System.Collections.Generic;
using System.Linq;
using SISHaU.Library.File.Model;
using System.IO;

namespace SISHaU.Library.File.Enginer
{
    public class OperationFile : IDisposable
    {
        public SplitFileModel SplitFile(string splitFileName)
        {
            var resultX = new SplitFileModel();
            
            //Используем поток файла не загружая оперативу, ненужными байтами
            using (var file = new FileStream(splitFileName, FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                resultX.FileInfo = new ResultModel
                {
                    FileName = Path.GetFileName(splitFileName),
                    FileSize = file.Length,
                    GostHash = file.FileGost()
                };

                resultX.AddParts(SplitFiles(file, Path.GetFileNameWithoutExtension(splitFileName)));
            }

            return resultX;
        }

        /// <summary>
        /// Собираем из результирующего объекта\коллекции объектов ExplodUnitModel, конечный файл.
        /// </summary>
        /// <param name="units">последовательнось типа ExplodUnitModel</param>
        /// <returns>Массив байт файла</returns>
        public byte[] CollectFile(IList<PrivateExplodUnitModel> units)
        {
            if (units == null || !units.Any()) return null;

            byte[] result = null;

            var count = units.Count();

            if (count > 1)
            {
                result = units.SelectMany(explodUnit => explodUnit.Unit).ToArray();
            }
            else if (count == 1)
            {
                result = units.FirstOrDefault()?.Unit;
            }

            return result;
        }

        private static IEnumerable<ByteDetectorModel> SplitFiles(Stream file, string fName)
        {
            var maxPartSize = (int)ConstantModel.MaxPartSize;
            
            var buffer = new byte[maxPartSize];
            var result = new List<ByteDetectorModel>();

            var partNumber = 1;
            var partLowerBound = 0;
            var partUpperBound = 0;

            while(true)
            {
                var partSize = file.Read(buffer, 0, maxPartSize);
                if (partSize == 0) break;

                var splitPartName = $@"{ConstantModel.TempPath}\{file.Length}_{fName}_{partNumber}.tmpart";
                using (var tmpFile = new FileStream(splitPartName, FileMode.Create, FileAccess.Write)){
                    tmpFile.Write(buffer, 0, partSize);
                }

                partUpperBound += partSize;

                result.Add(
                    new ByteDetectorModel
                    {
                        Part = partNumber++,
                        From = partLowerBound,
                        To = partUpperBound,
                        Patch = splitPartName,
                        Md5Hash = buffer.FileMd5()
                    });

                partLowerBound += partSize;

            }
            
            return result;
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}
