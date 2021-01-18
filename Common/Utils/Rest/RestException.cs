using System;

namespace Common.Utils.Rest
{
    public class RestException : Exception
    {
        public RestException(string s) : base(s) { }
    }
}
