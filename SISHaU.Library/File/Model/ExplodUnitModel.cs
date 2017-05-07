namespace SISHaU.Library.File.Model
{
    public class ExplodUnitModel : ByteDetectorModel
    {
        private byte[] _unit;

        public byte[] Unit {
            get => _unit;
            set
            {
                _unit = value;
                Marcer = new MarcerModel
                {
                    Md5Hash = Unit.FileMd5(),
                    GostHash = Unit.FileGost()
                };
            }
        }

        public MarcerModel Marcer { get; private set; }
    }
}
