using RestSharp;

namespace Common.Utils.Rest
{
    public class RestWrapper
    {
        public static IRestResponse GetResponse(RestRequestData restRequestData)
        {
            RestRequest request = new RestRequest(restRequestData.RequestPath, restRequestData.Method);
            request.Timeout = restRequestData.Timeout;
            request.AddHeader("Content-Type", "application/json");

            if (!string.IsNullOrWhiteSpace(restRequestData.Body))
                request.AddParameter("application/json", restRequestData.Body, ParameterType.RequestBody);

            if (restRequestData.Parameters != null && restRequestData.Parameters.Count > 0)
                foreach (var param in restRequestData.Parameters)
                    request.AddParameter(param);

            RestClient client = new RestClient(restRequestData.Host);
            IRestResponse response = client.Execute(request);

            return response;
        }
    }
}
