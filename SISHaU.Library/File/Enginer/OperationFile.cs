using System;
using System.Collections.Generic;
using System.Linq;
using SISHaU.Library.File.Model;

namespace SISHaU.Library.File.Enginer
{
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
                res.Add(new ByteDetectorModel { Part =  parts, From = from, To = to });
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
                        To = partToSize-1
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
            else if ( count == 1 )
            {
                result = units.FirstOrDefault()?.Unit;
            }
            
            return result;
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}
