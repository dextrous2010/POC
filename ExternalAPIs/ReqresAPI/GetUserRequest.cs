using Common.Utils.Rest;
using ExternalAPIs.ReqresAPI.Model;
using Newtonsoft.Json;
using RestSharp;
using System.Net;

namespace ExternalAPIs.ReqresAPI
{
    public class GetUserRequest : BaseReqresRequest
    {
        public static UserResponse GetUserResponse(int id)
        {
            UserResponse userResponse = null;
            var restRequestData = new RestRequestData
            {
                Host = Host,
                RequestPath = $"/api/users/{id}",
                Method = Method.GET
            };

            var response = RestWrapper.GetResponse(restRequestData);

            if (response.StatusCode == HttpStatusCode.OK && !string.IsNullOrWhiteSpace(response.Content.ToString()))
            {
                userResponse = JsonConvert.DeserializeObject<UserResponse>(response.Content);
            }
            //else throw new RestException($"Cannot get a user by {id}.\nResponse: {response.Content}");

            return userResponse;
        }

        public static UsersResponse GetUsersResponse(int page)
        {
            UsersResponse usersResponse = null;
            var restRequestData = new RestRequestData
            {
                Host = Host,
                RequestPath = $"/api/users?page{page}",
                Method = Method.GET
            };

            var response = RestWrapper.GetResponse(restRequestData);

            if (response.StatusCode == HttpStatusCode.OK && !string.IsNullOrWhiteSpace(response.Content.ToString()))
            {
                usersResponse = JsonConvert.DeserializeObject<UsersResponse>(response.Content);
            }
            else
                throw new RestException($"Cannot get a user by {page}.\nResponse: {response.Content}");

            return usersResponse;
        }
    }
}
