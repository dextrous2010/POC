using System.Collections.Generic;

namespace ExternalAPIs.ReqresAPI.Model
{
    public class UsersResponse
    {
        public int Page { get; set; }
        public int Per_Page { get; set; }
        public int Total { get; set; }
        public int Total_Pages { get; set; }
        public List<User> Data { get; set; }
        public Support Support { get; set; }
    }
}
