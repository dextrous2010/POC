using NUnit.Framework;
using SitesTesting.Browsers;

namespace SitesTesting.Helpers
{
    public class TestHelper
    {
        public static Browser InitializeBrowserInstance(BrowserType browserType, bool maximizeWindow = true)
        {
            switch (browserType)
            {
                case BrowserType.Chrome:
                    return new Chrome(maximizeWindow);
                case BrowserType.Firefox:
                    return new Firefox(maximizeWindow);
                default:
                    return new Chrome(maximizeWindow);
            }
        }

        public static string GetTestId() => TestContext.CurrentContext.Test.ID;
    }
}
