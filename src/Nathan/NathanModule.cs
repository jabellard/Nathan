using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Nathan.Abstractions;

namespace Nathan
{
    public class NathanModule
    {
        public NathanModuleDescriptor ModuleDescriptor { get; }

        public NathanModule()
        {
            ModuleDescriptor = new NathanModuleDescriptor(GetType());
        }

        protected void Base(Action<INathanModuleConfigurator> configuration)
        {
            var nathanModuleConfigurator = new NathanModuleConfigurator(ModuleDescriptor);
            configuration(nathanModuleConfigurator);
        }
        
        protected IEndpointConventionBuilder Options(string pathTemplate, NathanRequestDelegate handler)
        {
            return MapMethod(HttpMethods.Options, pathTemplate, handler);
        }
        
        protected IEndpointConventionBuilder Head(string pathTemplate, NathanRequestDelegate handler)
        {
            return MapMethod(HttpMethods.Head, pathTemplate, handler);
        }
        
        protected IEndpointConventionBuilder Get(string pathTemplate, NathanRequestDelegate handler)
        {
            return MapMethod(HttpMethods.Get, pathTemplate, handler);
        }
        
        protected IEndpointConventionBuilder Post(string pathTemplate, NathanRequestDelegate handler)
        {
            return MapMethod(HttpMethods.Post, pathTemplate, handler);
        }
        
        protected IEndpointConventionBuilder Put(string pathTemplate, NathanRequestDelegate handler)
        {
            return MapMethod(HttpMethods.Put, pathTemplate, handler);
        }
        
        protected IEndpointConventionBuilder Patch(string pathTemplate, NathanRequestDelegate handler)
        {
            return MapMethod(HttpMethods.Patch, pathTemplate, handler);
        }
        
        protected IEndpointConventionBuilder Delete(string pathTemplate, NathanRequestDelegate handler)
        {
            return MapMethod(HttpMethods.Delete, pathTemplate, handler);
        }

        protected IEndpointConventionBuilder MapMethod(string method, string pathTemplate, NathanRequestDelegate handler)
        {
            var deferredEndPointConventionBuilder = new DeferredEndPointConventionBuilder();
            ModuleDescriptor.AddHandlerDescriptor(method, pathTemplate, handler, deferredEndPointConventionBuilder);
            return deferredEndPointConventionBuilder;
        }
    }
}