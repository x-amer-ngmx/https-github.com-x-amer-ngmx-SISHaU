using System;

namespace SISHaU.Signature.Gis
{
    public class XadesInfo
    {
        public string Thumbprint { get; set; }
        public string RawPk { get; set; }
        public DateTime SigningDateTimeUtc { get; set; }
        public int TimeZoneOffsetMinutes { get; set; }
    }
}
