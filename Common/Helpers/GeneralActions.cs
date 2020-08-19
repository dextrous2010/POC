using NUnit.Framework;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Reflection;

namespace Common.Helpers
{
    public class GeneralActions
    {
        /// <summary>
        /// Returns an NUnit test method name
        /// </summary>
        /// <returns></returns>
        public static string GetTestMethodName()
        {
            try
            {
                return TestContext.CurrentContext.Test.Name;
            }
            catch
            {
                var stackTrace = new StackTrace();
                foreach (var stackFrame in stackTrace.GetFrames())
                {
                    MethodBase methodBase = stackFrame.GetMethod();
                    object[] attributes = methodBase.GetCustomAttributes(
                                              typeof(TestAttribute), false);
                    if (attributes.Length >= 1)
                    {
                        return methodBase.Name;
                    }
                }
                return string.Empty;
            }
        }

        public static string GetApplicationExecutionPath() => Path.GetDirectoryName(Assembly.GetExecutingAssembly().GetName().CodeBase).Substring(6);

        /// <summary>
        /// Returns a folder path to save a test report from app.config if configured of a default one
        /// </summary>
        /// <returns></returns>
        public static string GetReportFolderPath()
        {
            return !string.IsNullOrWhiteSpace(ConfigurationManager.AppSettings["ReportsFolder"]) ?
                    ConfigurationManager.AppSettings["ReportsFolder"]
                    : GetApplicationExecutionPath() + @"\..\..\..\Reports\";
        }

        /// <summary>
        /// Returns a file path to the extent-config.xml file from app.config if configured of a default one
        /// </summary>
        /// <returns></returns>
        public static string GetExtentConfigPath()
        {
            return !string.IsNullOrWhiteSpace(ConfigurationManager.AppSettings["ExtentConfigPath"]) ?
                    ConfigurationManager.AppSettings["ExtentConfigPath"]
                    : GetApplicationExecutionPath() + @"\extent-config.xml";
        }
    }
}
