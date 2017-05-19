namespace SISHaU.Library.File.Model
{
    public class ExplodUnitModel
    {
        private byte[] _unit;

        public ByteDetectorModel PartDetect { get; set; }

        public byte[] Unit
        {
            get => _unit;
            set
            {
                _unit = value;
                Md5Hash = _unit.FileMd5();

            }
        }

        public byte[] Md5Hash { get; private set; }
    }

    public class PrivateExplodUnitModel
    {
        public ByteDetectorModel PartDetect { get; set; }

        public byte[] Unit { get; set; }

    }
}
