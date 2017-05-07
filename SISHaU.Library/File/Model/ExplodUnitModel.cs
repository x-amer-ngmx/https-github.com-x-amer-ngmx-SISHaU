namespace SISHaU.Library.File.Model
{
    public abstract class ExplodUnitModel : ByteDetectorModel
    {
        private byte[] _unit;

        public byte[] Unit {
            get => _unit;
            set
            {
                _unit = value;
                Md5Hash = _unit.FileMd5();

            }
        }

        public byte[] Md5Hash{ get; private set; }
    }
}
