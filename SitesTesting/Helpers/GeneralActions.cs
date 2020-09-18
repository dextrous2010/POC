using System.Configuration;

namespace SitesTesting.Helpers
{
    public class GeneralActions
    {
        public static string GetApplicationExecutionPath() => System.AppDomain.CurrentDomain.BaseDirectory;

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
