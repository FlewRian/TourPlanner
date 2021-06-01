using System.Runtime.CompilerServices;

[assembly: log4net.Config.XmlConfigurator(Watch = true)]

namespace TourPlanner.Logger
{
    public class LogHelper
    {
        public static log4net.ILog GetLogger([CallerFilePath] string filename = "")
        {
            return log4net.LogManager.GetLogger(filename);
        }
    }
}
