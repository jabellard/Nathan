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
                .CreateApplicationBuilder().UseAuthorization()
                .ApplicationServices;
            var nathanApplicationConfigurator = new NathanApplicationConfigurator(applicationServiceProvider);
            nathanApplicationConfigurator
                .WithMiddleware<HandlerDispatchingMiddleware>(NathanPipelineComponents.HandlerDispatching);

            configuration(nathanApplicationConfigurator);
            
            var nathanApplicationBuilder = new NathanApplicationBuilder(applicationServiceProvider);
            var nathanApplicationConfiguration = nathanApplicationConfigurator.Configure();
            var nathanRequestDelegate = nathanApplicationBuilder
                .ConfigurePipeline(nathanApplicationConfiguration)
                .Build();
            
            var nathanModuleProvider = applicationServiceProvider
                .GetService<INathanModuleProvider>();
            var modules = nathanModuleProvider.GetModules();
            var endPointConventionBuilders = new List<IEndpointConventionBuilder>();
            foreach (var module in modules)
            {
                var handlerDescriptors = module.ModuleDescriptor.HandlerDescriptors;
                foreach (var handlerDescriptor in handlerDescriptors)
                {
                    var moduleDescriptor = handlerDescriptor.ParentModuleDescriptor;
                    var pathTemplate = handlerDescriptor.PathTemplate;
                    var methods = handlerDescriptor.Methods;
                    var endPointConventionBuilder = endpointRouteBuilder.MapMethods(pathTemplate, methods, async httpContext =>
                    {
                        httpContext.Items.Add(ObjectKeys.NathanRequestContextHandlerDescriptor, handlerDescriptor);
                        var nathanRequestContext = new NathanRequestContext(httpContext);
                        nathanRequestContext.Data.Add(ObjectKeys.NathanRequestContextHandlerDescriptor, handlerDescriptor);
                        await nathanRequestDelegate.Invoke(nathanRequestContext);
                    });
                    endPointConventionBuilder.WithMetadata(moduleDescriptor.MetaData);
                    handlerDescriptor.EndPointConventionBuilder.Apply(endPointConventionBuilder);
                    endPointConventionBuilder.WithMetadata(handlerDescriptor);
                    endPointConventionBuilders.Add(endPointConventionBuilder);
                }
            }
            return new AggregatedEndpointConventionBuilder(endPointConventionBuilders);
        }
    }
}