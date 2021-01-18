using Common.Utils.Rest;
using ExternalAPIs.ReqresAPI.Model;
using Newtonsoft.Json;
using RestSharp;
using System.Net;

namespace ExternalAPIs.ReqresAPI
{
    public class GetResourceRequest : BaseReqresRequest
    {
        public static ResourceResponse GetResponse(int id)
        {
            ResourceResponse resourceResponse = null;
            var restRequestData = new RestRequestData
            {
                Host = Host,
                RequestPath = $"/api/unknown/{id}",
                Method = Method.GET
            };

            var response = RestWrapper.GetResponse(restRequestData);

            if (response.StatusCode == HttpStatusCode.OK && !string.IsNullOrWhiteSpace(response.Content.ToString()))
            {
                resourceResponse = JsonConvert.DeserializeObject<ResourceResponse>(response.Content);
            }
            //else throw new RestException($"Cannot get a resource by {id}.\nResponse: {response.Content}");

            return resourceResponse;
        }

        public static ResourcesResponse GetResourcesResponse()
        {
            ResourcesResponse resourcesResponse = null;
            var restRequestData = new RestRequestData
            {
                Host = Host,
                RequestPath = $"/api/unknown",
                Method = Method.GET
            };

            var response = RestWrapper.GetResponse(restRequestData);

            if (response.StatusCode == HttpStatusCode.OK && !string.IsNullOrWhiteSpace(response.Content.ToString()))
            {
                resourcesResponse = JsonConvert.DeserializeObject<ResourcesResponse>(response.Content);
            }
            else
                throw new RestException($"Cannot get resourses.\nResponse: {response.Content}");

            return resourcesResponse;
        }
    }
}
