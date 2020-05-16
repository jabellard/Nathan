using System.Threading.Tasks;
using Nathan.Abstractions;

namespace Nathan.Middlewares
{
    public class NullMiddleware: INathanMiddleware
    {
        public async Task Invoke(NathanRequestContext nathanRequestContext, NathanRequestDelegate next)
        {
            await next(nathanRequestContext);
        }
    }
}