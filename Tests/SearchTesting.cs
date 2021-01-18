using NUnit.Framework;
using SitesTesting.Browsers;
using SitesTesting.Logging;
using SitesTesting.Model;
using SitesTesting.Sites;
using SitesTesting.Sites.Google;
using SitesTesting.Sites.Habr;

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

        [TestCaseSource(typeof(TestCaseData), "TitleContainsSearchedWord")]
        public void HabrSearchPageTitleContainsSearchedWord(string searchWord, int searchResultLinkToOpen, BrowserType browserType)
        {
            Log.WriteInfo("*** Starting the test ***");

            var title = new HabrMainPage(InitializeAndGetDriver(browserType))
                .GoToPage()
                .Search(searchWord)
                .OpenSearchResultLink(searchResultLinkToOpen)
                .Title;

            Assert.That(title.ToUpper().Contains(searchWord.ToUpper()), $"Title does not contain the searched word {searchWord}!");

            Log.WriteInfo("*** Test PASSED! ***");
        }


        [Test]
        public void LoginToHabrSuccess()
        {
            var browserType = BrowserType.Chrome;
            var user = new User()
            {
                Name = "DeXDen",
                Email = "dextrous2010@gmail.com",
                Password = "windows_13"
            };

            Log.WriteInfo("*** Starting the test ***");

            var mainPage = new HabrMainPage(InitializeAndGetDriver(browserType))
                .GoToPage();
            mainPage.Login(user);

            Assert.IsTrue(mainPage.IsUserLoggedIn(user), $"The user was not logged-in!");

            Log.WriteInfo("*** Test PASSED! ***");
        }
    }
}