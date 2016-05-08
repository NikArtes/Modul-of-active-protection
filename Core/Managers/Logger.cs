using System;
using System.IO;
using NLog;
using NLog.Config;
using NLog.Targets;

namespace Core.Managers
{
    public static class Logger
    {
        private static readonly NLog.Logger _defaultLogger = LogManager.GetCurrentClassLogger();

        private const string _layout = "[${date:format=dd/MM/yyyy HH\\:mm\\:ss.ffff}] ${level} - ${message}${onexception:${newline}${exception:format=ToString}${newline}${stacktrace:topFrames=10}}";

        public static void Info(string messageValue, string procName)
        {
            ConfigLogger(LogLevel.Info, AppConfigManager.GetPathToLogForProcess(procName));
            _defaultLogger.Info(messageValue);
        }

        public static void Error(string messageValue, string procName)
        {
            ConfigLogger(LogLevel.Error, AppConfigManager.GetPathToErrorForProcess(procName));
            _defaultLogger.Error(messageValue);
        }

        private static void ConfigLogger(LogLevel level, string path)
        {
            var config = new LoggingConfiguration();
            var fileTarget = new FileTarget();
            config.AddTarget("file", fileTarget);
            fileTarget.FileName = Path.Combine(path);
            fileTarget.Layout = _layout;
            fileTarget.CreateDirs = true;
            fileTarget.KeepFileOpen = true;
            config.LoggingRules.Add(new LoggingRule("*", level, fileTarget));
            LogManager.Configuration = config;
        }
    }
}