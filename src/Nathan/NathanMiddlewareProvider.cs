using System;
using Microsoft.Extensions.DependencyInjection;
using Nathan.Abstractions;

namespace Nathan
{
    public class NathanMiddlewareProvider: INathanMiddlewareProvider
    {
        private readonly IServiceProvider _serviceProvider;

        public NathanMiddlewareProvider(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }
        public INathanMiddleware GetMiddleware(Type middlewareType, params object[] parameters)
        {
            var nathanMiddleware = ActivatorUtilities.CreateInstance(_serviceProvider, middlewareType, parameters) as INathanMiddleware;
            return nathanMiddleware;
        }
    }
}