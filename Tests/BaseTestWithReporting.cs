using AventStack.ExtentReports;
using Common.Reporting;
using NUnit.Framework;
using NUnit.Framework.Interfaces;

namespace Tests
{
    public class BaseTestWithReporting
    {
        ExtentReports report;
        ExtentTest test;

        [OneTimeSetUp]
        public void InitializeReoprt()
        {
            report = ExtentManager.GetInstance();
        }

        [SetUp]
        public void CreateTestInReporter()
        {
            test = report.CreateTest(TestContext.CurrentContext.Test.MethodName);
        }

        [TearDown]
        public void LogTestStatusToReporter()
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
        }

        [OneTimeTearDown]
        public void CompleteReport()
        {
            report.Flush();
        }
    }
}
