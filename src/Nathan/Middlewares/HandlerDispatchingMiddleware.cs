using System.Threading.Tasks;
using Nathan.Abstractions;

namespace Nathan.Middlewares
{
    public class HandlerDispatchingMiddleware: INathanMiddleware
    {
        public Task Invoke(INathanRequestContext nathanRequestContext, NathanRequestDelegate next)
        {
            throw new System.NotImplementedException();
        }
    }
}