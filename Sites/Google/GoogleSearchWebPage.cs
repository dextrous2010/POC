using Common.Logging;
using OpenQA.Selenium;
using SeleniumExtras.PageObjects;
using SeleniumExtras.WaitHelpers;
using System;

namespace Sites.Google
{
    public class GoogleSearchWebPage : WebPage, ISearchWebPage
    {
        private const string URL = "https://www.google.com";

        [FindsBy(How = How.Name, Using = "q")]
        private IWebElement searchField;

        [FindsBy(How = How.XPath, Using = "//*[@class='aajZCb']//*[@name='btnK']")]
        private IWebElement searchGoogleBtn;


        public GoogleSearchWebPage(IWebDriver driver) : base(driver)
        {
        }

        public GoogleSearchWebPage GoToPage()
        {
            Log.WriteInfo($"Opening Google search page {URL}");
            driver.Navigate().GoToUrl(URL);
            return this;
        }

        public bool IsInitialized() => driver.Url.ToLower().Contains(URL);

        public ISearchResultsWebPage Search(string text)
        {
            Log.WriteInfo($"Searching for the text \"{text}\"");
            bool searchBtnClicked = false;
            searchField.SendKeys(text);
            try
            {
                driverWait.Until(ExpectedConditions.ElementToBeClickable(searchGoogleBtn));
                searchBtnClicked = true;
            }
            catch (Exception)
            {
                Log.WriteInfo($"Cannot find the search button. Will try to press Enter.");
            }

            if (!searchBtnClicked)
                searchField.SendKeys(Keys.Enter);

            searchGoogleBtn.Click();
            return new GoogleSearchResultsWebPage(driver);
        }
    }
}
