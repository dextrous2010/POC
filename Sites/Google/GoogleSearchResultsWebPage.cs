using Common.Logging;
using OpenQA.Selenium;
using SeleniumExtras.PageObjects;
using SeleniumExtras.WaitHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Sites.Google
{
    public class GoogleSearchResultsWebPage : WebPage, ISearchResultsWebPage
    {
        private int currentPageNumber = 0;
        private const int lastPageNumber = 10;

        [FindsBy(How = How.Name, Using = "q")]
        private IWebElement searchField;

        [FindsBy(How = How.XPath, Using = "//*[@id='pnnext']/span[2]")]
        private IWebElement nextPageBtn;

        [FindsBy(How = How.XPath, Using = "//*[@id='pnprev']/span[2]")]
        private IWebElement previousPageBtn;

        [FindsBy(How = How.XPath, Using = "//div[@class='r']/a")]
        private IList<IWebElement> searchResultLinks;

        public GoogleSearchResultsWebPage(IWebDriver driver) : base(driver)
        {
            Log.WriteInfo("Opened a web page with the serch results.");
            currentPageNumber++;
        }

        public bool IsInitialized() => driver.Url.ToLower().Contains("https://www.google.com/search?");

        public ISearchResultsWebPage GoToNextPage()
        {
            Log.WriteInfo("Opening the next page with the serch results.");
            if (currentPageNumber >= lastPageNumber)
            {
                Log.WriteError($"Cannot navigate to the next page, since you are already on the last page.\n{Environment.StackTrace}");
                throw new ApplicationException("Cannot navigate to the next page, since you are already on the last page.");
            }

            driverWait.Until(ExpectedConditions.ElementToBeClickable(nextPageBtn));
            nextPageBtn.Click();
            return this;
        }

        public ISearchResultsWebPage GoToPreviousPage()
        {
            Log.WriteInfo("Opening the previous page with the serch results.");
            if (currentPageNumber <= 1)
            {
                Log.WriteError($"Cannot navigate to the previous page, since you are already on the {currentPageNumber} page.\n{Environment.StackTrace}");
                throw new ApplicationException($"Cannot navigate to the previous page, since you are already on the {currentPageNumber} page.");
            }

            driverWait.Until(ExpectedConditions.ElementToBeClickable(previousPageBtn));
            previousPageBtn.Click();
            return this;
        }

        public WebPage OpenSearchResultLink(int linkNumer)
        {
            Log.WriteInfo($"Opening the {linkNumer} results link");
            driverWait.Until(ExpectedConditions.ElementToBeClickable(searchResultLinks[linkNumer - 1]));
            searchResultLinks[linkNumer - 1].Click();
            return new WebPage(driver);
        }

        public bool ContainsDomain(string domain)
        {
            Log.WriteInfo($"Checking if there is the domain {domain} on the current web results page.");

            var regPattern = $@"https?:\/\/{domain}";

            return searchResultLinks.Any(
                i => Regex.Match(i.GetAttribute("href"), regPattern).Success);
        }
    }
}
