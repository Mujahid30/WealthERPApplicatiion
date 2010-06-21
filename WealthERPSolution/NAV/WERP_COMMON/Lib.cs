using NLog;
namespace WERP_COMMON
{
    public class Lib
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();
        public static void ExitApplication()
        {
            logger.Debug("Exiting appliation");
            System.Environment.Exit(0);
        }

    }
}
