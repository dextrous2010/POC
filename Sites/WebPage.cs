using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.PageObjects;
using System;

namespace Sites
{
    public class WebPage
    {
        protected IWebDriver driver;
        protected WebDriverWait driverWait;
        public string Title => driver.Title;

        public WebPage(IWebDriver driver)
        {
            this.driver = driver;
            this.driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            driverWait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            PageFactory.InitElements(driver, this);
        }

        public void SetImplicitWait(int driverWaitSec)
        {
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(driverWaitSec);
        }

        public void SetWebDriverWait(int driverWaitSec)
        {
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(driverWaitSec);
        }
    }
}
