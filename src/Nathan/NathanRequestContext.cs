using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Nathan.Abstractions;

namespace Nathan
{
    public class NathanRequestContext: INathanRequestContext
    {
        public HttpContext HttpContext { get;}
        public Dictionary<string, object> Data { get; }

        public NathanRequestContext(HttpContext httpContext)
        {
            HttpContext = httpContext;
            Data = new Dictionary<string, object>();
        }
    }
}