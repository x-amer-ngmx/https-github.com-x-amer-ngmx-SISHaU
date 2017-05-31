using System.Collections.Generic;

namespace SISHaU.Library.File.Model
{
    public class SplitFileModel
    {
        public ResultModel FileInfo { get; set; }
        public IList<ByteDetectorModel> Parts { get; set; }
        public RequestErrorModel ErrorMessage { get; set; }

        public SplitFileModel(){
            Parts = new List<ByteDetectorModel>();
        }

        public void AddParts(IEnumerable<ByteDetectorModel> parts)
        {
            foreach (var part in parts)
            {
                Parts.Add(part);
            }
        }
    }
}
