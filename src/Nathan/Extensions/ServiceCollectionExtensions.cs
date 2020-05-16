using System;
using Microsoft.Extensions.DependencyInjection;

namespace Nathan.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddNathan(this IServiceCollection services, Action<NathanServiceConfigurator> configuration)
        {
            var nathanServiceConfigurator = new NathanServiceConfigurator(services);
            configuration(nathanServiceConfigurator);
            nathanServiceConfigurator.Configure();
            return services;
        }
    }
}