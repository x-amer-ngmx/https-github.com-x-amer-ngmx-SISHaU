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
        /// <param name="filePrefix">Часть пути с именем файла без расширения</param>
        /// <returns>Возвращает объект с данными для формирование MessageRequest</returns>
        public SplitFileModel SplitFile(string splitFileName, string filePrefix)
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
                result.AddParts(SplitFiles(file, filePrefix));
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
        /// <param name="filePartPrefix">Наименование файла без расширения и часть пути к папке Temp</param>
        /// <returns>Возвращает объект с данными для формирование MessageRequest</returns>
        private IEnumerable<UpPartInfoModel> SplitFiles(Stream file, string filePartPrefix)
        {
            var result = new List<UpPartInfoModel>();

            var partNumber = 1;

            var buffer = new byte[Config.MaxPartSize];            

            while (true)
            {
                var partSize = file.Read(buffer, 0, (int)Config.MaxPartSize);
                if (partSize == 0) break;

                var splitPartName = $"{filePartPrefix}_{partNumber:D2}_{file.Length}.tmpart";
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
