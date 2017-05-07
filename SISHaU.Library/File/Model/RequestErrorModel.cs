using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SISHaU.Library.File.Model
{
    public class RequestErrorModel
    {
        public int ErrorCode { get; set; }

        public string ErrorInfo { get; set; }

        public string PointErrorDescript { get; set; }
    }
}
