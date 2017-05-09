using log4net;
using log4net.Config;

namespace SISHaU.Library
{
    public static class Logger
    {
        private static ILog _log = LogManager.GetLogger("LOGGER");

        public static ILog Log
        {
            get { return _log; }
        }

        public static void InitLogger()
        {
            XmlConfigurator.Configure();
        }
    }
}
