using System.Collections.Generic;

namespace SISHaU.Library.File.Model
{
    public class SplitFileModel
    {
        public ResultModel FileInfo { get; set; }
        public IList<UpPartInfoModel> Parts { get; set; }
        public RequestErrorModel ErrorMessage { get; set; }

        public SplitFileModel(){
            Parts = new List<UpPartInfoModel>();
        }

        public void AddParts(IEnumerable<UpPartInfoModel> parts)
        {
            foreach (var part in parts)
            {
                Parts.Add(part);
            }
        }
    }
}
