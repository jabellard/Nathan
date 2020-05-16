using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Nathan.Abstractions;

namespace Nathan.Middlewares
{
    public class HandlerDispatchingMiddleware: INathanMiddleware
    {
        public async Task Invoke(NathanRequestContext nathanRequestContext, NathanRequestDelegate next)
        {
            var httpContext = nathanRequestContext.HttpContext;
            var endPoint = httpContext.GetEndpoint();
            endPoint.RequestDelegate(httpContext);
        }
    }
}