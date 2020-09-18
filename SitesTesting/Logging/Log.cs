using NLog;
using NUnit.Framework;

namespace SitesTesting.Logging
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
            var name = TestContext.CurrentContext.Test.Name;
            Instance.Info(!string.IsNullOrEmpty(name) ? $"TEST | {name} | {message}" : message);
        }

        public static void WriteError(string message)
        {
            var name = TestContext.CurrentContext.Test.Name;
            Instance.Error(!string.IsNullOrEmpty(name) ? $"TEST | {name} | {message}" : message);
        }
    }
}
