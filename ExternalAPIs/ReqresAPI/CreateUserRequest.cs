using Common.Utils.Rest;
using ExternalAPIs.ReqresAPI.Model;
using Newtonsoft.Json;
using RestSharp;
using System.Net;

namespace ExternalAPIs.ReqresAPI
{
    public class CreateUserRequest : BaseReqresRequest
    {
        public static NewUserCreateResponse GetResponse(NewUser user)
        {
            NewUserCreateResponse newUserCreateResp = null;
            var restRequestData = new RestRequestData
            {
                Host = Host,
                RequestPath = $"/api/users",
                Method = Method.POST,
                Body = JsonConvert.SerializeObject(user)
            };

            var response = RestWrapper.GetResponse(restRequestData);

            if (response.StatusCode == HttpStatusCode.Created && !string.IsNullOrWhiteSpace(response.Content.ToString()))
            {
                newUserCreateResp = JsonConvert.DeserializeObject<NewUserCreateResponse>(response.Content);
            }
            //else throw new RestException($"Cannot create a user.\nResponse: {response.Content}");

            return newUserCreateResp;
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
