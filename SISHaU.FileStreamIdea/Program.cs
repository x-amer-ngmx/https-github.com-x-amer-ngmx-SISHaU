using SISHaU.Library.File;
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
                @"D:\test6.zip",
                //@"D:\test7.zip", //350mb
                //@"D:\test8.zip" //1 386 mb
            };

            var tmpPath = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);
            var result = new List<UploadeResultModel>();

            foreach (var patch in files)
            {
                result.Add(SplitFiles(tmpPath, patch));
            }


            foreach (var re in result)
            {
                using (var tmpFile = new FileStream($@"{tmpPath}\{re.FileName}", FileMode.Create, FileAccess.Write))
                {
                    foreach (var pat in re.Parts)
                    {
                        var buff = File.ReadAllBytes(pat.Patch);

                        tmpFile.Write(buff, 0, buff.Length);

                        if (pat.Patch.IndexOf(".tmpart") > 0) File.Delete(pat.Patch);
                    }
                }
            }
        }

        /// <summary>
        /// Операция расщипления файла. Получает поток файла и определяет больше ли он заданного предела или нет, и возвращает путь либо к самому файлу либо к его нарезанным частям...
        /// </summary>
        /// <param name="tmpPath">Можно вынести в константу, так как это локальное хранилище временных частей файла.</param>
        /// <param name="patch">Путь к файлу</param>
        /// <returns>Объект определяющий размер и массив частей.</returns>
        private static UploadeResultModel SplitFiles(string tmpPath, string patch)
        {
            var resultX = new UploadeResultModel();
            IList<ByteDetectorModel> result = null;
            var fName = Path.GetFileNameWithoutExtension(patch);

            resultX.FileName = Path.GetFileName(patch);

            //Используем поток файла не загружая оперативу, ненужными байтами
            using (var file = new FileStream(patch, FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                //Определяем кол-во частей
                var parts = (int)(file.Length / ConstantModel.MaxPartSize) + 1;
                
                //Применение рефакторинг-кунгфу....
                result = parts == 1 ? new ByteDetectorModel[] {
                        new ByteDetectorModel{
                            From = 0,
                            To = file.Length,
                            Part = 1,
                            Patch = patch,
                            Md5Hash = file.FileMd5()
                        }
                    } : SingleFiles(parts, file, fName, tmpPath);
                
                //Определяем и сохраняем хеш-по-госту файла
                resultX.FileGuid = file.FileGost();
                file.Dispose();
            }

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

                //выделение буферной памяти для создания части
                var buffer = new byte[buffSize];
                //запись части в буфер и возврат её реального размера
                var partSize = file.Read(buffer, 0, buffSize);

                //путь к временно-созданной части
                var pat = $@"{tmpPath}\{file.Length}_{fName}.{part}.tmpart";

                //Создание части, если часть уже существует то она будет перезаписанна
                using (var tmpFile = new FileStream(pat, FileMode.Create, FileAccess.Write))
                {
                    tmpFile.Write(buffer, 0, partSize);
                }

                //Формируем коллекцию частей(в языке C# несуществует простых массивов)
                result.Add(new ByteDetectorModel { Part = part, From = partTo, To = from - 1, Patch = pat, Md5Hash = buffer.FileMd5() });
                part++;
            }

            return result;
        }

    }
}
