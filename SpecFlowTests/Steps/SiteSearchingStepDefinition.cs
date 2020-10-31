using TechTalk.SpecFlow;
using SitesTesting.Sites.Google;
using SitesTesting.Helpers;
using SitesTesting.Browsers;
using OpenQA.Selenium;
using NUnit.Framework;

namespace SpecFlowTests.Steps
{
    [Binding]
    public sealed class SiteSearchingStepDefinition
    {
        private readonly ScenarioContext _scenarioContext;

        private BrowserType BrowserType { get; set; }
        private string SearchWord { get; set; }
        private int LinkNumber { get; set; }
        private string Title { get; set; }

        private string SearchDomain { get; set; }
        private int SearchPagesCount { get; set; }
        private bool SearchedRresultsContainDomain { get; set; }


        public SiteSearchingStepDefinition(ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;
        }

        [Given("the browser type is (.*)")]
        public void GivenTheBrowserTypeIs(BrowserType browserType)
        {
            BrowserType = browserType;
        }

        [Given("the search word is (.*)")]
        public void GivenTheSearchWordIs(string searchWord)
        {
            SearchWord = searchWord;
        }

        [Given("the search result link number to open is (.*)")]
        public void GivenTheSearchResultLinkNumberToOpenIs(int linkNumber)
        {
            LinkNumber = linkNumber;
        }

        [When("the search result link is opened")]
        public void WhenTheSearchResultLinkIsOpened()
        {
            IWebDriver driver = TestHelper.InitializeBrowserInstance(BrowserType).Driver;
            Title = new GoogleSearchWebPage(driver)
                .GoToPage()
                .Search(SearchWord)
                .OpenSearchResultLink(LinkNumber)
                .Title;

            driver.Quit();
        }

        [Then("the title should contain searched world (.*)")]
        public void ThenTheTitleShouldContainSearchedWorld(string searchWord)
        {
            Assert.That(Title.ToUpper().Contains(searchWord.ToUpper()), $"Title does not contain the searched word {searchWord}!");
        }


        [Given("the search domain is (.*)")]
        public void GivenTheSearchDomainIs(string searchDomain)
        {
            SearchDomain = searchDomain;
        }

        [Given("the search pages count is (.*)")]
        public void GivenTheSearchDomainIs(int searchPagesCount)
        {
            SearchPagesCount = searchPagesCount;
        }

        [When("the search is done")]
        public void WhenTheSearchIsDone()
        {
            IWebDriver driver = TestHelper.InitializeBrowserInstance(BrowserType).Driver;

            SearchedRresultsContainDomain = new GoogleSearchWebPage(driver)
                .GoToPage()
                .Search(SearchWord)
                .SearchedResultsContainDomain(SearchDomain, SearchPagesCount);

            driver.Quit();
        }

        [Then("the searched results should contain domain (.*)")]
        public void ThenTheSearchedRresultsShouldContainDomain(string searchWord)
        {
            Assert.That(SearchedRresultsContainDomain, $"There is no the expected domain on the first {SearchPagesCount} pages!");
        }
    }
}
