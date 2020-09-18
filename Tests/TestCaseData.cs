using SitesTesting.Browsers;
using SitesTesting.Helpers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Tests
{
    public class TestCaseData
    {
        public static IEnumerable<object> TitleContainsSearchedWord
        {
            get
            {
                var path = GeneralActions.GetApplicationExecutionPath() + @"\..\..\TestDataFiles\TitleContainsSearchedWord.txt";
                var data = File.ReadAllLines(path)
                    .Skip(1)
                    .Where(s => !string.IsNullOrWhiteSpace(s));

                foreach (var item in data)
                {
                    yield return new object[] { item.Split(';')[0], int.Parse(item.Split(';')[1]), Enum.Parse(typeof(BrowserType), item.Split(';')[2]) };
                }
            }
        }

        public static IEnumerable<object> SearchedResultsContainDomain
        {
            get
            {
                var filePath = GeneralActions.GetApplicationExecutionPath() + @"\..\..\TestDataFiles\SearchedResultsContainDomain.txt";
                var data = File.ReadAllLines(filePath)
                    .Skip(1)
                    .Where(s => !string.IsNullOrWhiteSpace(s));

                foreach (var item in data)
                {
                    yield return new object[] { item.Split(';')[0], item.Split(';')[1], int.Parse(item.Split(';')[2]), Enum.Parse(typeof(BrowserType), item.Split(';')[3]) };
                }
            }
        }
    }
}
