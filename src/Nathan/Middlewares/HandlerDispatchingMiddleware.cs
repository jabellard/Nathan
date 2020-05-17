using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Nathan.Abstractions;

namespace Nathan.Middlewares
{
    public class HandlerDispatchingMiddleware: INathanMiddleware
    {
        public async Task Invoke(NathanRequestContext nathanRequestContext, NathanRequestDelegate next)
        {
            var httpContext = nathanRequestContext.HttpContext;
            var storedHandlerDescriptor = nathanRequestContext.HandlerDescriptor;
            var nathanModule = httpContext
                .RequestServices
                .GetRequiredService(storedHandlerDescriptor.ParentModuleDescriptor.ModuleType) as NathanModule;
            var nathanHandler = nathanModule
                .ModuleDescriptor
                .HandlerDescriptors
                .First(d => d.Key == storedHandlerDescriptor.Key)
                .Value
                .RequestDelegate;
            await nathanHandler(nathanRequestContext);
        }
    }
}