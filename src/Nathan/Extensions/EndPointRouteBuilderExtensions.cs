using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using Nathan.Abstractions;
using Nathan.Middlewares;

namespace Nathan.Extensions
{
    public static class EndPointRouteBuilderExtensions
    {
        public static IEndpointConventionBuilder MapNathan(this IEndpointRouteBuilder endpointRouteBuilder, Action<INathanApplicationConfigurator> configuration)
        {
            var applicationServiceProvider = endpointRouteBuilder
                .CreateApplicationBuilder()
                .ApplicationServices;
            var nathanApplicationConfigurator = new NathanApplicationConfigurator(applicationServiceProvider);
            nathanApplicationConfigurator
                .WithMiddleware<HandlerDispatchingMiddleware>(NathanPipelineComponents.HandlerDispatching);

            var nathanFeatureProvider = applicationServiceProvider
                .GetRequiredService<INathanFeatureProvider>();
            var nathanFeatures = nathanFeatureProvider.GetFeatures();
            foreach (var nathanFeature in nathanFeatures)
                nathanFeature.Configure(nathanApplicationConfigurator);

            configuration(nathanApplicationConfigurator);
            
            var nathanApplicationBuilder = new NathanApplicationBuilder(applicationServiceProvider);
            var nathanApplicationConfiguration = nathanApplicationConfigurator.Configure();
            var nathanRequestDelegate = nathanApplicationBuilder
                .Build(nathanApplicationConfiguration);
            
            var nathanModuleProvider = applicationServiceProvider
                .GetService<INathanModuleProvider>();
            var modules = nathanModuleProvider.GetModules();
            var endPointConventionBuilders = new List<IEndpointConventionBuilder>();
            foreach (var module in modules)
            {
                var handlerDescriptors = module.ModuleDescriptor.HandlerDescriptors.Values;
                foreach (var handlerDescriptor in handlerDescriptors)
                {
                    var moduleDescriptor = handlerDescriptor.ParentModuleDescriptor;
                    var pathTemplate = handlerDescriptor.PathTemplate;
                    var methods = new []{handlerDescriptor.Method};
                    var endPointConventionBuilder = endpointRouteBuilder.MapMethods(pathTemplate, methods, async httpContext =>
                    {
                        httpContext.Items.Add(NathanObjectKeys.HandlerDescriptor, handlerDescriptor);
                        var nathanRequestContext = new NathanRequestContext(httpContext);
                        await nathanRequestDelegate.Invoke(nathanRequestContext);
                    });
                    endPointConventionBuilder.WithMetadata(moduleDescriptor.MetaData);
                    handlerDescriptor.EndPointConventionBuilder.Execute(endPointConventionBuilder);
                    endPointConventionBuilder.WithMetadata(handlerDescriptor);
                    endPointConventionBuilders.Add(endPointConventionBuilder);
                }
            }
            return new AggregatedEndpointConventionBuilder(endPointConventionBuilders);
        }
    }
}