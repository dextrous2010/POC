using OpenQA.Selenium;
using OpenQA.Selenium.Remote;

namespace SitesTesting.Browsers
{
    public class Browser
    {
        public IWebDriver Driver { get; }

        public Browser(RemoteWebDriver remoteWebDriver)
        {
            Driver = remoteWebDriver;
        }

        public Browser(RemoteWebDriver remoteWebDriver, bool maximizeWindow = true)
        {
            Driver = remoteWebDriver;

            if (maximizeWindow)
                Driver.Manage().Window.Maximize();
        }
    }
}
