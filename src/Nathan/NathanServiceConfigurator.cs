using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Nathan.Abstractions;

namespace Nathan
{
    public class NathanServiceConfigurator: INathanServiceConfigurator
    {
        public IServiceCollection Services { get; }
        private readonly Lazy<ServiceProvider> _lazyServiceProvider;
        private readonly IDictionary<string, Tuple<Type, DateTime>> _keyedFeatureTypeRegistrations;
        private ServiceProvider ServiceProvider => _lazyServiceProvider.Value;

        internal NathanServiceConfigurator(IServiceCollection services)
        {
            Services = services;
            _lazyServiceProvider = new Lazy<ServiceProvider>(services.BuildServiceProvider);
            _keyedFeatureTypeRegistrations = new Dictionary<string, Tuple<Type, DateTime>>();
        }

        public INathanServiceConfigurator WithOptions(IConfigurationSection configuration)
        {
            Services.Configure<NathanOptions>(configuration);
            return this;
        }

        public INathanServiceConfigurator WithOptions(Action<NathanOptions> configuration)
        {
            Services.Configure(configuration);
            return this;
        }
        
        public INathanServiceConfigurator WithFeature<TFeature>() where TFeature : INathanFeature
        {
            var featureType = typeof(TFeature);
            _keyedFeatureTypeRegistrations.Add(featureType.FullName, new Tuple<Type, DateTime>(featureType, DateTime.UtcNow));
            return this;
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
            
            var featureRegistrations = _keyedFeatureTypeRegistrations
                .Select(ktr => new NathanFeatureRegistration
                {
                    Feature = ActivatorUtilities.CreateInstance(ServiceProvider, ktr.Value.Item1) as INathanFeature,
                    Timestamp = ktr.Value.Item2
                })
                .ToList();
            
            featureRegistrations
                .ForEach(fr =>
                {
                    fr.Feature.ConfigureServices(Services);
                    Services.AddSingleton<INathanFeatureRegistration>(fr);
                });
            
            if (_lazyServiceProvider.IsValueCreated)
                _lazyServiceProvider.Value.Dispose();
        }
    }
}