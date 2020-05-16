using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Microsoft.Extensions.DependencyModel;

namespace Nathan
{
    public class NathanComponentProvider
    {
        private readonly DependencyContext _dependencyContext;

        public NathanComponentProvider()
            : this(Assembly.GetEntryAssembly())
        {
            
        }
        
        public NathanComponentProvider(Assembly assembly)
        {
            _dependencyContext = DependencyContext.Load(assembly);
        }

        public IEnumerable<Type> GetModules()
        {
            var assemblies = GetAssemblies();
            var modules = assemblies
                .SelectMany(a => a.GetTypes()
                    .Where(t => t.IsPublic &&
                                !t.IsAbstract &&
                                t != typeof(NathanModule) &&
                                typeof(NathanModule).IsAssignableFrom(t)
                                ));
            return modules;
        }

        private IEnumerable<Assembly> GetAssemblies()
        {
            var assemblies = new HashSet<Assembly>();
            foreach (var runtimeLibrary in _dependencyContext.RuntimeLibraries)
                foreach (var assemblyName in runtimeLibrary.GetDefaultAssemblyNames(_dependencyContext))
                {
                    var assembly = Assembly.Load(assemblyName);
                    assemblies.Add(assembly);
                }
            return assemblies;
        }
    }
}