using NUnit.Framework;
using ExternalAPIs.ReqresAPI;
using ExternalAPIs.ReqresAPI.Model;

namespace Tests.ExternalAPIsTesting
{
    [TestFixture]
    public class Reqres
    {
        #region Users tests

        [Test]
        public void GetListOfUsers([Range(1, 2)] int page)
        {
            var userRespo = GetUserRequest.GetUsersResponse(page);
            Assert.That(userRespo.Data.Count > 0, "There is no list of users!");
        }

        [Test]
        public void GetExistingUser([Range(1, 12)] int id)
        {
            var userRespo = GetUserRequest.GetUserResponse(id);
            Assert.That(userRespo.Data != null, "User does not exist!");
        }

        [Test]
        public void GetUserNotFound([Values(13)] int id)
        {
            var userRespo = GetUserRequest.GetUserResponse(id);
            Assert.That(userRespo == null, "User does exist!");
        }

        #endregion

        #region Resources tests

        [Test]
        public void GetListOfResources()
        {
            var resourcesRespo = GetResourceRequest.GetResourcesResponse();
            Assert.That(resourcesRespo.Data.Count > 0, "There is no list of Resources!");
        }

        [Test]
        public void GetExistingResource([Range(1, 12)] int id)
        {
            var resourceRespo = GetResourceRequest.GetResponse(id);
            Assert.That(resourceRespo.Data != null, "Resource does not exist!");
        }

        [Test]
        public void GetResourceNotFound([Values(13)] int id)
        {
            var resourceRespo = GetResourceRequest.GetResponse(id);
            Assert.That(resourceRespo == null, "Resource does exist!");
        }

        #endregion

        [Test]
        public void CreateUserSuccess()
        {
            var newUserResp = CreateUserRequest.GetResponse(
                new NewUser
                {
                    Name = "Test",
                    Job = "QA"
                }
            );
            Assert.That(newUserResp.Id > 0, "New user was not created!");
        }

        [Test]
        public void RegisterUserSuccess()
        {
            var regUserResp = RegisterUserRequest.GetResponse(
                new RegisterUser
                {
                    Email = "eve.holt@reqres.in",
                    Password = "pistol"
                }
            );
            Assert.That(regUserResp.Id > 0, "User was NOT registered!");
        }
    }
}
