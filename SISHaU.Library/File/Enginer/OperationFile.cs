using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SISHaU.Library.File.Model;

namespace SISHaU.Library.File.Enginer
{
    public class OperationFile
    {
        /// <summary>
        /// Разделяем полученны поток файла 
        /// </summary>
        /// <param name="file">Массив байт файла</param>
        /// <returns>object (ExplodUnitModel or List<ExplodUnitModel>)</returns>
        public object ExplodingFile(byte[] file)
        {
            long sizeFile = file.Length;
            var modes = sizeFile % ConstantModel.MaxPartSize;
            var mod = modes == 0;

            var parts = (int)(sizeFile / ConstantModel.MaxPartSize);
            var pprs = !mod ? parts + 1 : parts;

            var res = new List<ByteDetectorModel>();
            if (pprs <= 1)
            {
                return
                    new ExplodUnitModel
                    {
                        Unit = file
                    };

            }

            parts = pprs;


            while (parts > 0)
            {
                var xxc = parts == pprs ? modes : ConstantModel.MaxPartSize;
                var from = (int)xxc;
                var to = (int)(sizeFile > ConstantModel.MaxPartSize ? (sizeFile - xxc) : 0);
                res.Add(new ByteDetectorModel { Part =  parts, From = to, To = from - 1 });
                sizeFile = to;
                parts--;
            }

            res = (from resx in res orderby resx.Part ascending select resx).ToList();
            var result = res.Select(detector => new ExplodUnitModel
            {
                Part = detector.Part,
                From = detector.From,
                To = detector.To,
                Unit = file.Skip(detector.From).Take(detector.To + 1).ToArray()
            }).ToList();

            return result;
        }

        /// <summary>
        /// Собираем из результирующего объекта\ коллекции объектов ExplodUnitModel, конечный файл.
        /// </summary>
        /// <param name="units">object (IEnumerable<ExplodUnitModel> or ExplodUnitModel)</param>
        /// <returns>Массив байт файла</returns>
        public byte[] CollectFile(object units)
        {
            if (units == null) return null;

            byte[] result = null;
            if (units is IEnumerable<ExplodUnitModel>)
            {
                result = ((IEnumerable<ExplodUnitModel>)units).SelectMany(explodUnit => explodUnit.Unit).ToArray();
            }
            else if (units is ExplodUnitModel)
            {
                result = ((ExplodUnitModel)units).Unit;
            }
            
            return result;
        }
    }
}
