namespace SitesTesting.Sites
{
    public interface ISearchResultsWebPage
    {
        ISearchResultsWebPage GoToNextPage();
        ISearchResultsWebPage GoToPreviousPage();
        WebPage OpenSearchResultLink(int linkNumer);
        bool ContainsDomain(string domain);
        bool SearchedResultsContainDomain(string domain, int searchPagesCount);
    }
}
