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
}
