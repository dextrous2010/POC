using OpenQA.Selenium.Chrome;

namespace Browsers
{
    public class Chrome : Browser
    {
        public Chrome() : base(new ChromeDriver())
        {
        }

        public Chrome(bool maximizeWindow = false) : base(new ChromeDriver(), maximizeWindow)
        {
        }
    }
}
