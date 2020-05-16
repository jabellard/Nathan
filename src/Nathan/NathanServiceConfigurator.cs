using Microsoft.Extensions.DependencyInjection;

namespace Nathan
{
    public class NathanServiceConfigurator
    {
        public IServiceCollection Services { get; }

        internal NathanServiceConfigurator(IServiceCollection services)
        {
            Services = services;
        }

        internal void Configure()
        {
            Services.AddLogging();
            Services.AddOptions();
            Services.AddRouting();
            var nathanComponentProvider = new NathanComponentProvider();
            var modules = nathanComponentProvider.GetModules();
            foreach (var module in modules)
            {
                Services.AddScoped(module);
                Services.AddScoped(typeof(NathanModule), module);
            }
        }
    }
}