using Browsers;
using Common.Helpers;
using Common.Logging;
using NUnit.Framework;
using Sites.Google;
using System;

namespace Tests
{
    [TestFixture]
    [Parallelizable(ParallelScope.All)]
    public class Tests : BaseTestWithReporting
    {
        [TestCaseSource(typeof(TestCaseData), "TitleContainsSearchedWord")]
        public void TitleContainsSearchedWord(string searchWord, BrowserType browserType)
        {
            var linkNumber = 1;

            Log.WriteInfo("*** Starting the test ***");

            Browser browser = TestHelper.InitializeBrowserInstance(browserType, true);
            try
            {
                var searchPage = new GoogleSearchWebPage(browser.Driver).GoToPage();
                Assert.True(searchPage.IsInitialized(), "The search page is not initizlized!");

                var title = searchPage.Search(searchWord)
                    .OpenSearchResultLink(linkNumber)
                    .Title;

                Assert.That(title.ToUpper().Contains(searchWord.ToUpper()), $"Title does not contain the searched word {searchWord}!");

                Log.WriteInfo("*** Test PASSED! ***");
            }
            catch (AssertionException)
            {
                Log.WriteInfo($"*** Test FAILED ***");
                throw;
            }
            catch (Exception ex)
            {
                Log.WriteError($"An error occured during the test execuiton.\n{ex}");
                throw;
            }
            finally
            {
                browser.Driver.Quit();
            }
        }

        [TestCaseSource(typeof(TestCaseData), "SearchedResultsContainDomain")]
        public void SearchedResultsContainDomain(string searchWord, string searchDomain, BrowserType browserType)
        {
            var searchPagesCount = 5;

            Log.WriteInfo("*** Starting the test ***");

            Browser browser = TestHelper.InitializeBrowserInstance(browserType, true);
            try
            {
                var searchPage = new GoogleSearchWebPage(browser.Driver).GoToPage();
                Assert.True(searchPage.IsInitialized(), "The search page is not initizlized!");

                var searchResultsPage = searchPage.Search(searchWord);
                Assert.True(searchResultsPage.IsInitialized(), "The search results page is not initizlized!");

                for (int page = 1; page <= searchPagesCount; page++)
                {
                    if (page == 1 ? searchResultsPage.ContainsDomain(searchDomain) : searchResultsPage.GoToNextPage().ContainsDomain(searchDomain))
                        Assert.Pass($"There is the expected domain on the first {searchPagesCount} pages.");
                }

                Assert.Fail($"There is no the expected domain on the first {searchPagesCount} pages!");

                Log.WriteInfo("*** Test PASSED! ***");
            }
            catch (AssertionException)
            {
                Log.WriteInfo($"*** Test FAILED ***");
                throw;
            }
            catch (Exception ex)
            {
                Log.WriteError($"An error occured during the test execuiton.\n{ex}");
                throw;
            }
            finally
            {
                browser.Driver.Quit();
            }
        }
    }
}