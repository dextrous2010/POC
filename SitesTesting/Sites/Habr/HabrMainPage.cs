using OpenQA.Selenium;
using SeleniumExtras.PageObjects;
using SeleniumExtras.WaitHelpers;
using SitesTesting.Logging;
using SitesTesting.Model;
using System;

namespace SitesTesting.Sites.Habr
{
    public class HabrMainPage : WebPage, ISearchWebPage
    {
        private const string URL = "https://habr.com/ru";
        private const string ExpectedTitle = "Все публикации подряд / Хабр";

        [FindsBy(How = How.Id, Using = "search-form-btn")]
        private IWebElement searchBtn;

        [FindsBy(How = How.Id, Using = "search-form-field")]
        private IWebElement searchField;

        [FindsBy(How = How.Id, Using = "login")]
        private IWebElement loginBtn;

        [FindsBy(How = How.CssSelector, Using = "div[class='dropdown dropdown_user'] > button")]
        private IWebElement loggedUserIcon;

        public HabrMainPage(IWebDriver driver) : base(driver) { }

        public HabrMainPage GoToPage()
        {
            Log.WriteInfo($"Opening Habr main page {URL}");
            driver.Navigate().GoToUrl(URL);

            if (!Title.ToLower().Contains(ExpectedTitle.ToLower()))
            {
                Log.WriteError($"The Habr page was not opened!{Environment.NewLine}{Environment.StackTrace}");
                throw new NotFoundException("The Habr page was not opened!");
            }

            return this;
        }

        public ISearchResultsWebPage Search(string text)
        {
            Log.WriteInfo($"Searching for the text \"{text}\"");
            searchBtn.Click();
            try
            {
                driverWait.Until(ExpectedConditions.ElementToBeClickable(searchField));
                driverWait.Until(ExpectedConditions.ElementToBeClickable(searchField));
                Log.WriteInfo("The search field is shown");
            }
            catch (Exception)
            {
                throw new NotFoundException($"There is no the search field.");
            }

            searchField.SendKeys(text);
            searchField.SendKeys(Keys.Enter);

            return new HabrSearchResults(driver);
        }

        public HabrMainPage Login(User user)
        {
            loginBtn.Click();
            new HabrLoginPage(driver).Login(user);
            return this;
        }

        public bool IsUserLoggedIn(User user)
        {
            bool isLoggedIn = false;
            try
            {

                if (loggedUserIcon.GetAttribute("title").ToLower() == user.Name.ToLower())
                {
                    Log.WriteInfo("The user is logged-in");
                    isLoggedIn = true;
                }
            }
            catch (Exception)
            {
                Log.WriteInfo("The user is not logged-in");
            }
            return isLoggedIn;
        }
    }
}
