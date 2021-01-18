using ExternalAPIs.ReqresAPI;
using ExternalAPIs.ReqresAPI.Model;
using NUnit.Framework;
using System.Collections.Generic;
using TechTalk.SpecFlow;

namespace SpecFlowTests.Steps
{
    [Binding]
    public sealed class ReqresAPIs
    {
        private readonly ScenarioContext _scenarioContext;

        private int Page { get; set; }
        private List<User> Data { get; set; }

        private string UserName { get; set; }
        private string UserJob { get; set; }
        private int UserId { get; set; }

        public ReqresAPIs(ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;
        }

        [Given("the user page number is (.*)")]
        public void GivenTheUserPageIs(int page)
        {
            Page = page;
        }

        [When("send the users request")]
        public void WhenSendUsersRequest()
        {
            Data = GetUserRequest.GetUsersResponse(Page).Data;
        }

        [Then("the count of returned users should be > (.*)")]
        public void ThenTheCountOfUsersShouldBeMoreThan(int count)
        {
            Assert.That(Data.Count > count, "There is no list of users!");
        }

        [When("send the resources request")]
        public void WhenSendResoucesRequest()
        {
            Data = GetUserRequest.GetUsersResponse(Page).Data;
        }

        [Then("the count of returned resources should be > (.*)")]
        public void ThenTheCountOfResourcesShouldBeMoreThan(int count)
        {
            Assert.That(Data.Count > count, "There is no list of resources!");
        }

        [Given("the user name is (.*)")]
        public void GivenTheUserName(string name)
        {
            UserName = name;
        }

        [Given("the user's job is (.*)")]
        public void GivenTheUserJob(string job)
        {
            UserJob = job;
        }

        [When("send the create user request")]
        public void WhenSendCreateUserRequest()
        {
            UserId = CreateUserRequest.GetResponse(
                 new NewUser
                 {
                     Name = UserName,
                     Job = UserJob
                 }
             ).Id;
        }

        [Then("the user should get an id")]
        public void ThenUserShouldGetAnId()
        {
            Assert.That(UserId > 0, "New user was not created!");
        }
    }
}
