using System.Collections.Generic;

namespace SISHaU.Library.File.Model
{
    public class ByteDetectorModel
    {
        public int Part { get; set; }
        public long From { get; set; }
        public long To { get; set; }
        public string Patch { get; set; }
        public byte[] Md5Hash { get; set; }
    }

    public class PartInfoModel
    {
        public int Part { get; set; }
        public string Patch { get; set; }
    }

    public class DownloadInfoModel
    {
        public ResultModel FileInfo { get; set; }
        public IList<PartInfoModel> PartInfo { get; set; }
        public RequestErrorModel ErrorMessage { get; set; }
    }

    public class UpPartInfoModel : PartInfoModel
    {
        public byte[] Md5Hash { get; set; }
    }

    public class DownPartInfoModel : RangeModel
    {
        public int Part { get; set; }
    }

}
