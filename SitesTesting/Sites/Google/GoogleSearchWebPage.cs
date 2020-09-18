using OpenQA.Selenium;
using SeleniumExtras.PageObjects;
using SeleniumExtras.WaitHelpers;
using SitesTesting.Logging;
using System;

namespace SitesTesting.Sites.Google
{
    public class GoogleSearchWebPage : WebPage, ISearchWebPage
    {
        private const string URL = "https://www.google.com";
        private const string ExpectedTitle = "Google";

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

            if (!Title.ToLower().Contains(ExpectedTitle.ToLower()))
            {
                Log.WriteError($"The Google search page was not opened!{Environment.NewLine}{Environment.StackTrace}");
                throw new NotFoundException("The Google search page was not opened!");
            }

            return this;
        }

        public ISearchResultsWebPage Search(string text)
        {
            Log.WriteInfo($"Searching for the text \"{text}\"");
            bool searchBtnClicked = false;
            searchField.SendKeys(text);
            try
            {
                driverWait.Until(ExpectedConditions.ElementToBeClickable(searchGoogleBtn));
                searchGoogleBtn.Click();
                searchBtnClicked = true;
            }
            catch (Exception)
            {
                Log.WriteInfo($"Cannot find the search button. Will try to press Enter.");
            }

            if (!searchBtnClicked)
                searchField.SendKeys(Keys.Enter);

            return new GoogleSearchResultsWebPage(driver);
        }
    }
}
