using System;
using System.Collections.Generic;
using Microsoft.Extensions.DependencyInjection;
using Nathan.Abstractions;

namespace Nathan
{
    public class NathanModuleProvider : INathanModuleProvider
    {
        private readonly IServiceProvider _serviceProvider;

        public NathanModuleProvider(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }
        
        public IEnumerable<NathanModule> GetModules()
        {
            var modules = _serviceProvider.GetServices<NathanModule>();
            return modules;
        }
    }
}