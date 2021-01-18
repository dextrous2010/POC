using OpenQA.Selenium;
using SeleniumExtras.PageObjects;
using SeleniumExtras.WaitHelpers;
using SitesTesting.Logging;
using SitesTesting.Model;

namespace SitesTesting.Sites.Habr
{
    public class HabrLoginPage : WebPage, ILoginPage
    {
        [FindsBy(How = How.Id, Using = "email_field")]
        private IWebElement emailField;

        [FindsBy(How = How.Id, Using = "password_field")]
        private IWebElement passwordField;

        [FindsBy(How = How.XPath, Using = "//button[@type='submit']")]
        private IWebElement submitBtn;

        public HabrLoginPage(IWebDriver driver) : base(driver) { }

        public void Login(User user)
        {
            Log.WriteInfo("Trying to login to the Habr...");
            emailField.SendKeys(user.Email);
            passwordField.SendKeys(user.Password);

            driverWait.Until(ExpectedConditions.ElementToBeClickable(submitBtn));
            IJavaScriptExecutor executor = (IJavaScriptExecutor)driver;
            executor.ExecuteScript("arguments[0].click();", submitBtn);
        }
    }
}
