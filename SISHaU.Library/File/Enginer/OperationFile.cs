using System;
using System.Collections.Generic;
using System.Linq;
using SISHaU.Library.File.Model;
using System.IO;

namespace SISHaU.Library.File.Enginer
{
    public class OperationFile : IDisposable
    {
        /// <summary>
        /// Обработка коллекции путей к файлам
        /// </summary>
        /// <param name="splitFileName">Путь к файлу</param>
        /// <returns>Возвращает объект с данными для формирование MessageRequest</returns>
        public SplitFileModel SplitFile(string splitFileName, string temp)
        {
            var result = new SplitFileModel();

            //Используем поток файла не загружая оперативу, ненужными байтами
            using (var file = new FileStream(splitFileName, FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                result.FileInfo = new ResultModel
                {
                    FileName = Path.GetFileName(splitFileName),
                    FileSize = file.Length,
                    GostHash = file.FileGost()
                };
                file.Seek(0, SeekOrigin.Begin);

                result.AddParts(SplitFiles(file, temp, Path.GetFileNameWithoutExtension(splitFileName)));
            }

            return result;
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


        #region Локальные методы
        /// <summary>
        /// Операция расщипления потока файла на буферные части
        /// </summary>
        /// <param name="file">Поток файла</param>
        /// <param name="fName">Наименование файла с расширением</param>
        /// <returns>Возвращает объект с данными для формирование MessageRequest</returns>
        private IEnumerable<UpPartInfoModel> SplitFiles(Stream file,string temp, string fName)
        {
            var result = new List<UpPartInfoModel>();

            var partNumber = 1;

            var buffer = new byte[Config.MaxPartSize];            

            while (true)
            {
                var partSize = file.Read(buffer, 0, (int)Config.MaxPartSize);
                if (partSize == 0) break;

                var splitPartName = $@"{temp}\{fName}_{partNumber:D2}_{file.Length}.tmpart";
                using (var tmpFile = new FileStream(splitPartName, FileMode.Create, FileAccess.Write))
                {
                    tmpFile.Write(buffer, 0, partSize);
                }

                result.Add(
                    new UpPartInfoModel
                    {
                        Part = partNumber++,
                        Patch = splitPartName,
                        Md5Hash = buffer.FileMd5(partSize)
                    });
            }

            return result;
        }
        #endregion

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}
