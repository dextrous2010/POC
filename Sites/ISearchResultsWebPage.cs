namespace Sites
{
    public interface ISearchResultsWebPage
    {
        bool IsInitialized();
        ISearchResultsWebPage GoToNextPage();
        ISearchResultsWebPage GoToPreviousPage();
        WebPage OpenSearchResultLink(int linkNumer);
        bool ContainsDomain(string domain);
    }
}
