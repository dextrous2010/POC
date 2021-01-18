using OpenQA.Selenium;
using SeleniumExtras.PageObjects;
using SeleniumExtras.WaitHelpers;
using SitesTesting.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace SitesTesting.Sites.Habr
{
    public class HabrSearchResults : WebPage, ISearchResultsWebPage
    {
        private const string URL = "https://habr.com/ru/search/?";
        private int currentPageNumber = 0;

        [FindsBy(How = How.Id, Using = "next_page")]
        private IWebElement nextPageBtn;

        [FindsBy(How = How.Id, Using = "previous_page")]
        private IWebElement previousPageBtn;

        [FindsBy(How = How.Id, Using = "nav-pagess")]
        private IList<IWebElement> navigationPages;

        [FindsBy(How = How.ClassName, Using = "post__title")]
        private IList<IWebElement> searchResultLinks;

        public HabrSearchResults(IWebDriver driver) : base(driver)
        {
            if (!driver.Url.ToLower().Contains(URL.ToLower()))
            {
                Log.WriteError($"The Habr page with search results was not opened!\nUrl => {driver.Url}{Environment.NewLine}{Environment.StackTrace}");
                throw new NotFoundException("The Habr page with search results was not opened!");
            }

            Log.WriteInfo("Opened a web page with the serch results.");
            currentPageNumber++;
        }

        public ISearchResultsWebPage GoToNextPage()
        {
            Log.WriteInfo("Opening the next page with the serch results.");
            if (currentPageNumber >= navigationPages.Count)
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

        public bool SearchedResultsContainDomain(string searchDomain, int searchPagesCount)
        {
            while (searchPagesCount-- > 0)
            {
                if (ContainsDomain(searchDomain))
                    return true;

                if (searchPagesCount != 0)
                    GoToNextPage();
            };

            return false;
        }
    }
}
