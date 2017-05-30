using System;
using System.Collections.Generic;
using System.Linq;
using SISHaU.Library.File.Model;
using System.IO;

namespace SISHaU.Library.File.Enginer
{
    //TODO: OperationFile - необходим рефакторинг...
    public class OperationFile : IDisposable
    {
        /// <summary>
        /// Разделяем полученны поток файла 
        /// </summary>
        /// <param name="file">Массив байт файла</param>
        /// <returns>коллекцию типа ExplodUnitModel</returns>
        public List<ExplodUnitModel> ExplodingFile(byte[] file)
        {

            long sizeFile = file.Length;
            var modes = sizeFile % ConstantModel.MaxPartSize;
            var mod = modes == 0;

            var parts = (int)(sizeFile / ConstantModel.MaxPartSize);
            var pprs = !mod ? parts + 1 : parts;


            if (pprs <= 1)
            {
                return new List<ExplodUnitModel>
                {
                    new ExplodUnitModel
                    {
                        Unit = file
                    }
                };

            }

            var res = new List<ByteDetectorModel>();

            parts = pprs;

            while (parts > 0)
            {
                var partTo = parts == pprs ? modes : ConstantModel.MaxPartSize;
                var to = (int)partTo;
                var from = (int)(sizeFile > ConstantModel.MaxPartSize ? sizeFile - partTo : 0);
                res.Add(new ByteDetectorModel { Part = parts, From = from, To = to });
                sizeFile = from;
                parts--;
            }

            res = (from resx in res orderby resx.Part select resx).ToList();

            var result = new List<ExplodUnitModel>();

            long partFromSize = 0;
            foreach (var detector in res)
            {
                var thisPartSize = (partFromSize + ConstantModel.MaxPartSize);
                var partToSize = modes > thisPartSize ? modes : thisPartSize;
                result.Add(new ExplodUnitModel
                {
                    PartDetect = new ByteDetectorModel
                    {
                        Part = detector.Part,
                        From = partFromSize,
                        To = partToSize - 1
                    },
                    Unit = file.Skip((int)detector.From).Take((int)detector.To).ToArray()
                });

                partFromSize = thisPartSize;
            }

            return result;
        }

        /// <summary>
        /// Собираем из результирующего объекта\коллекции объектов ExplodUnitModel, конечный файл.
        /// </summary>
        /// <param name="units">последовательнось типа ExplodUnitModel</param>
        /// <returns>Массив байт файла</returns>
        public byte[] CollectFile(IEnumerable<PrivateExplodUnitModel> units)
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


        public SplitFileModel SplitFile(string tmpPath, string patch)
        {
            var resultX = new SplitFileModel();
            IList<ByteDetectorModel> result = null;
            var fName = Path.GetFileNameWithoutExtension(patch);

            ResultModel fInfo;
            //Используем поток файла не загружая оперативу, ненужными байтами
            using (var file = new FileStream(patch, FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                fInfo = new ResultModel
                {
                    FileName = Path.GetFileName(patch),
                    FileSize = file.Length
                };

                //Определяем кол-во частей
                var parts = (int)(file.Length / ConstantModel.MaxPartSize) + 1;

                //Применение рефакторинг-кунгфу....
                result = parts == 1 ? new List<ByteDetectorModel> {
                        new ByteDetectorModel{
                            From = 0,
                            To = file.Length,
                            Part = 1,
                            Patch = patch,
                            Md5Hash = file.FileMd5()
                        }
                    } : SingleFiles(parts, file, fName, tmpPath);

                //Определяем и сохраняем хеш-по-госту файла
                resultX.GostHash = file.FileGost();
                file.Dispose();
            }

            resultX.FileInfo = fInfo;
            resultX.Parts = result;

            return resultX;
        }

        private static IList<ByteDetectorModel> SingleFiles(int parts, Stream file, string fName, string tmpPath)
        {
            var result = new List<ByteDetectorModel>();
            var part = 1;
            long partTo = 0;

            while (part <= parts)
            {
                partTo = part == 1 ? 0 : partTo + ConstantModel.MaxPartSize;

                var from = part != parts ? partTo + ConstantModel.MaxPartSize : file.Length;
                var buffSize = part != parts ? (int)(ConstantModel.MaxPartSize) : (int)(file.Length - partTo);

                //выделение буферной памяти для создания части
                var buffer = new byte[buffSize];
                //запись части в буфер и возврат её реального размера
                var partSize = file.Read(buffer, 0, buffSize);

                //путь к временно-созданной части
                var splitPatch = $@"{tmpPath}\{file.Length}_{fName}.{part}.tmpart";

                //Создание части, если часть уже существует то она будет перезаписанна
                using (var tmpFile = new FileStream(splitPatch, FileMode.Create, FileAccess.Write))
                {
                    tmpFile.Write(buffer, 0, partSize);
                }

                //Формируем коллекцию частей(в языке C# несуществует простых массивов)
                result.Add(
                    new ByteDetectorModel
                    {
                        Part = part,
                        From = partTo,
                        To = from - 1,
                        Patch = splitPatch,
                        Md5Hash = buffer.FileMd5()
                    });
                part++;
            }

            return result;
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}
