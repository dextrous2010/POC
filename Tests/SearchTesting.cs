using NUnit.Framework;
using SitesTesting.Browsers;
using SitesTesting.Logging;
using SitesTesting.Sites.Google;

namespace Tests
{
    [TestFixture]
    [Parallelizable(ParallelScope.All)]
    public class Tests : BaseTestWithReporting
    {
        [TestCaseSource(typeof(TestCaseData), "TitleContainsSearchedWord")]
        public void TitleContainsSearchedWord(string searchWord, int searchResultLinkToOpen, BrowserType browserType)
        {
            Log.WriteInfo("*** Starting the test ***");

            var title = new GoogleSearchWebPage(InitializeAndGetDriver(browserType))
                .GoToPage()
                .Search(searchWord)
                .OpenSearchResultLink(searchResultLinkToOpen)
                .Title;

            Assert.That(title.ToUpper().Contains(searchWord.ToUpper()), $"Title does not contain the searched word {searchWord}!");

            Log.WriteInfo("*** Test PASSED! ***");
        }

        [TestCaseSource(typeof(TestCaseData), "SearchedResultsContainDomain")]
        public void SearchedResultsContainDomain(string searchWord, string searchDomain, int searchPagesCount, BrowserType browserType)
        {
            Log.WriteInfo("*** Starting the test ***");

            var searchResultsPage = new GoogleSearchWebPage(InitializeAndGetDriver(browserType))
                .GoToPage()
                .Search(searchWord);

            Assert.That(searchResultsPage.SearchedResultsContainDomain(searchDomain, searchPagesCount), $"There is no the expected domain on the first {searchPagesCount} pages!");

            Log.WriteInfo("*** Test PASSED! ***");
        }
    }
}