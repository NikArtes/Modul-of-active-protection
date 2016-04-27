using NLog;

namespace LoggerModule
{
    public static class Logger
    {
        private static readonly NLog.Logger _defaultLogger = LogManager.GetLogger("default");

        public static void Info(string messageValue)
        {
            _defaultLogger.Info(messageValue);
        }

        public static void Error(string messageValue)
        {
            _defaultLogger.Error(messageValue);
        }
    }
}