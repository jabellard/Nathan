using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Nathan.Abstractions;

namespace Nathan
{
    public class NathanApplicationBuilder
    {
        private readonly IList<Func<NathanRequestDelegate, NathanRequestDelegate>> _middlewares;
        public IServiceProvider ApplicationServiceProvider { get; }

        public NathanApplicationBuilder(IServiceProvider applicationServiceProvider)
        {
            ApplicationServiceProvider = applicationServiceProvider;
            _middlewares = new List<Func<NathanRequestDelegate, NathanRequestDelegate>>();
        }
        
        public NathanApplicationBuilder UseMiddleware<TNathanMiddleware>(params object[] parameters) 
            where TNathanMiddleware : INathanMiddleware
        {
            return UseMiddleware(typeof(TNathanMiddleware), parameters);
        }

        public NathanApplicationBuilder UseMiddleware(Type middlewareType, params object[] parameters)
        {
            return Use(next =>
            {
                var nathanMiddlewareProvider = ApplicationServiceProvider.GetService<INathanMiddlewareProvider>();
                var nathanMiddleware = nathanMiddlewareProvider.GetMiddleware(middlewareType, parameters);
                return async trafficContext => await nathanMiddleware.Invoke(trafficContext, next);
            });
        }

        public NathanApplicationBuilder Use(Func<NathanRequestDelegate, NathanRequestDelegate> middleware)
        {
           _middlewares.Add(middleware);
           return this;
        }
        

        public NathanRequestDelegate Build(NathanApplicationConfiguration nathanApplicationConfiguration)
        {
            ConfigurePipeline(nathanApplicationConfiguration);
            NathanRequestDelegate application = nathanContext => Task.CompletedTask;
            foreach (var middleware in _middlewares)
                application = middleware(application);
            return application;
        }
        
        private void ConfigurePipeline(NathanApplicationConfiguration nathanApplicationConfiguration)
        {
            var middlewareRegistrations = nathanApplicationConfiguration.MiddlewareRegistrations;
            UseRegistrationSection(middlewareRegistrations, NathanPipelineComponents.HandlerDispatching);
        }
        
        private void UseRegistrationSection(IDictionary<string, MiddlewareRegistration> middlewareRegistrations, string sectionKey)
        {
            var preMiddlewareRegistrations = middlewareRegistrations
                .Where(r => r.Key.Contains($"Pre{sectionKey}/"))
                .OrderBy(r => r.Value.Timestamp)
                .Select(r => r.Value);
            foreach (var preMiddlewareRegistration in preMiddlewareRegistrations)
                UseRegistration(preMiddlewareRegistration);
            
            UseRegistration(middlewareRegistrations[sectionKey]);
            
            var postMiddlewareRegistrations = middlewareRegistrations
                .Where(r => r.Key.Contains($"Post{sectionKey}/"))
                .OrderBy(r => r.Value.Timestamp)
                .Select(r => r.Value);
            foreach (var postMiddlewareRegistration in postMiddlewareRegistrations)
                UseRegistration(postMiddlewareRegistration);
        }

        private void UseRegistration(MiddlewareRegistration middlewareRegistration)
        {
            switch (middlewareRegistration.Type)
            {
                case MiddlewareType.Delegate:
                    Use(middlewareRegistration.Middleware as Func<NathanRequestDelegate, NathanRequestDelegate>);
                    break;
                case MiddlewareType.Class:
                    UseMiddleware(middlewareRegistration.Middleware as Type, middlewareRegistration.Parameters);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}