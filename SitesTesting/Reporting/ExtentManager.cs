using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;
using SitesTesting.Helpers;

namespace SitesTesting.Reporting
{
    public class ExtentManager
    {
        public static ExtentHtmlReporter htmlReporter;
        public static ExtentReports extentReoprts;

        public static ExtentReports GetInstance()
        {
            if (extentReoprts == null)
            {
                htmlReporter = new ExtentHtmlReporter(GeneralActions.GetReportFolderPath());
                extentReoprts = new ExtentReports();
                extentReoprts.AttachReporter(htmlReporter);
                extentReoprts.AddSystemInfo("OS", "Windows");

                htmlReporter.LoadConfig(GeneralActions.GetExtentConfigPath());
            }
            return extentReoprts;
        }
    }
}
