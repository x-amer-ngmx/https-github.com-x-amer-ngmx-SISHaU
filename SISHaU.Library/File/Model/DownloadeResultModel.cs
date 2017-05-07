using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SISHaU.Library.File.Model
{
    public class DownloadResultModel : ResultModel
    {
        public byte[] FileBytes { get; set; }

        public RequestErrorModel ErrorMessage { get; set; }
    }
}
