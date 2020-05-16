using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Nathan.Abstractions
{
    public interface INathanServiceConfigurator
    {
        IServiceCollection Services { get; }
        INathanServiceConfigurator WithOptions(IConfigurationSection configuration);
        INathanServiceConfigurator WithOptions(Action<NathanOptions> configuration); 
        INathanServiceConfigurator WithFeature<TFeature>()
            where TFeature: INathanFeature;
        
    }
}