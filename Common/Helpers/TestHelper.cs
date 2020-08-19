using Browsers;

namespace Common.Helpers
{
    public class TestHelper
    {
        public static Browser InitializeBrowserInstance(BrowserType browserType, bool maximizeWindow = false)
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
    }
}
