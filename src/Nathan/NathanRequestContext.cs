using System.Collections.Generic;
using Microsoft.AspNetCore.Http;

namespace Nathan
{
    public class NathanRequestContext
    {
        public HttpContext HttpContext { get;}

        public NathanHandlerDescriptor HandlerDescriptor =>
            HttpContext.Items[NathanObjectKeys.HandlerDescriptor] as NathanHandlerDescriptor;
        public Dictionary<string, object> Data { get; }

        public NathanRequestContext(HttpContext httpContext)
        {
            HttpContext = httpContext;
            Data = new Dictionary<string, object>();
        }
    }
}