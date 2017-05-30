using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SISHaU.Library.File.Model
{
    public class DownloadModel
    {
        public Repo Repository { get; set; }
        public string FileGuid { get; set; }
    }

    public class PrivateDownloadModel
    {
        public ResultModel FileInfo { get; set; }

        /// <summary>
        /// Параметр принимает значение в случае если файл превышает 5мб и соответственно разделён на части
        /// </summary>
        public IList<PrivateExplodUnitModel> Parts { get; set; }

        public RequestErrorModel ErrorMessage { get; set; }
    }

    public class PrivateExplodUnitModel
    {
        public ByteDetectorModel PartDetect { get; set; }

        public byte[] Unit { get; set; }

    }

}
