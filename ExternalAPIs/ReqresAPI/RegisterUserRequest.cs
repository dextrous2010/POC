using Common.Utils.Rest;
using ExternalAPIs.ReqresAPI.Model;
using Newtonsoft.Json;
using RestSharp;
using System.Net;

namespace ExternalAPIs.ReqresAPI
{
    public class RegisterUserRequest : BaseReqresRequest
    {
        public static RegisterUserResponse GetResponse(RegisterUser user)
        {
            RegisterUserResponse regUserResp = null;
            var restRequestData = new RestRequestData
            {
                Host = Host,
                RequestPath = $"/api/register",
                Method = Method.POST,
                Body = JsonConvert.SerializeObject(user)
            };

            var response = RestWrapper.GetResponse(restRequestData);

            if (response.StatusCode == HttpStatusCode.OK && !string.IsNullOrWhiteSpace(response.Content.ToString()))
            {
                regUserResp = JsonConvert.DeserializeObject<RegisterUserResponse>(response.Content);
            }
            //else throw new RestException($"Cannot register a user.\nResponse: {response.Content}");

            return regUserResp;
        }
    }
}
