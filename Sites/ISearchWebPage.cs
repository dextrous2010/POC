﻿namespace Sites
{
    public interface ISearchWebPage
    {
        ISearchResultsWebPage Search(string text);
        bool IsInitialized();
    }
}
