using RestSharp;
using System.Collections.Generic;

namespace Common.Utils.Rest
{
    public class RestRequestData
    {
        public string Host { get; set; }
        public string RequestPath { get; set; }
        public string Body { get; set; }
        public Method Method { get; set; }
        public List<Parameter> Parameters { get; set; }
        public int Timeout { get; set; }
    }
}
