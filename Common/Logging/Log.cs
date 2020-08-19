using Common.Helpers;
using NLog;
using System;

namespace Common.Logging
{
    public class Log
    {
        private static Logger Instance { get; }

        static Log()
        {
            Instance = LogManager.GetCurrentClassLogger();
        }

        public static void WriteInfo(string message)
        {
            var name = GeneralActions.GetTestMethodName();
            Instance.Info(!string.IsNullOrEmpty(name) ? $"TEST | {name} | {message}" : message);
        }

        public static void WriteInfo(Exception exception, string message)
        {
            var name = GeneralActions.GetTestMethodName();
            Instance.Info(exception, !string.IsNullOrEmpty(name) ? $"TEST | {name} | {message}" : message);
        }

        public static void WriteError(string message)
        {
            var name = GeneralActions.GetTestMethodName();
            Instance.Error(!string.IsNullOrEmpty(name) ? $"TEST | {name} | {message}" : message);
        }

        public static void WriteError(Exception exception, string message)
        {
            var name = GeneralActions.GetTestMethodName();
            Instance.Error(exception, !string.IsNullOrEmpty(name) ? $"TEST | {name} | {message}" : message);
        }
    }
}
