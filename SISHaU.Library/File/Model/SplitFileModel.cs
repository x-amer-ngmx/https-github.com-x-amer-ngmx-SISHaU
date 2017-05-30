using System.Collections.Generic;

namespace SISHaU.Library.File.Model
{
    public class SplitFileModel
    {
        public ResultModel FileInfo { get; set; }
        public IList<ByteDetectorModel> Parts { get; set; }
        public string GostHash { get; set; }
        public RequestErrorModel ErrorMessage { get; set; }
    }
}
