using AventStack.ExtentReports;
using NUnit.Framework;
using NUnit.Framework.Interfaces;
using OpenQA.Selenium;
using SitesTesting.Browsers;
using SitesTesting.Helpers;
using SitesTesting.Reporting;
using System.Collections.Generic;

namespace Tests
{
    public class BaseTestWithReporting
    {
        private Dictionary<string, IWebDriver> driverPerTestId;
        private ExtentReports report;
        private ExtentTest test;

        public BaseTestWithReporting()
        {
        }

        [OneTimeSetUp]
        protected void Initialization()
        {
            driverPerTestId = new Dictionary<string, IWebDriver>();
            report = ExtentManager.GetInstance();
        }

        [SetUp]
        protected void CreateTestInReporter()
        {
            test = report.CreateTest(TestContext.CurrentContext.Test.MethodName);
        }

        [TearDown]
        protected void LogTestStatusToReporter()
        {
            if (TestContext.CurrentContext.Result.Outcome.Status == TestStatus.Passed)
                test.Log(Status.Pass, $"Test {TestContext.CurrentContext.Test.MethodName} Passed");
            if (TestContext.CurrentContext.Result.Outcome.Status == TestStatus.Failed)
                test.Log(Status.Fail, $"Test {TestContext.CurrentContext.Test.MethodName} Failed");
            if (TestContext.CurrentContext.Result.Outcome.Status == TestStatus.Skipped)
                test.Log(Status.Skip, $"Test {TestContext.CurrentContext.Test.MethodName} Skipped");
            if (TestContext.CurrentContext.Result.Outcome.Status == TestStatus.Inconclusive)
                test.Log(Status.Skip, $"Test {TestContext.CurrentContext.Test.MethodName} Inconclusive");
            if (TestContext.CurrentContext.Result.Outcome.Status == TestStatus.Warning)
                test.Log(Status.Warning, $"Test {TestContext.CurrentContext.Test.MethodName} Warning");

            DisposeTestDriver();
        }

        [OneTimeTearDown]
        protected void Finalization()
        {
            report.Flush();
        }

        protected IWebDriver InitializeAndGetDriver(BrowserType browserType)
        {
            var driver = TestHelper.InitializeBrowserInstance(browserType).Driver;
            driverPerTestId.Add(TestHelper.GetTestId(), driver);
            return driver;
        }

        protected void DisposeTestDriver()
        {
            driverPerTestId[TestHelper.GetTestId()].Quit();
            driverPerTestId.Remove(TestHelper.GetTestId());
        }
    }
}
