using System;
using Nathan.Abstractions;

namespace Nathan
{
    public class NathanApplicationConfigurator: INathanApplicationConfigurator
    {
        public IServiceProvider ApplicationServiceProvider { get; }
        public NathanApplicationConfiguration Configuration { get; }

        public NathanApplicationConfigurator(IServiceProvider applicationServiceProvider)
        {
            ApplicationServiceProvider = applicationServiceProvider;
            Configuration = new NathanApplicationConfiguration();
        }
        public INathanApplicationConfigurator WithPreMiddleware(string key, Func<NathanRequestDelegate, NathanRequestDelegate> middleware)
        {
            var preKey = $"Pre{key}/{Guid.NewGuid()}";
            Configuration.MiddlewareRegistrations.Add(preKey, new MiddlewareRegistration
            {
                Middleware = middleware,
                Type = MiddlewareType.Delegate
            });
            return this;
        }

        public INathanApplicationConfigurator WithMiddleware(string key, Func<NathanRequestDelegate, NathanRequestDelegate> middleware)
        {
            Configuration.MiddlewareRegistrations.Add(key, new MiddlewareRegistration
            {
                Middleware = middleware,
                Type = MiddlewareType.Delegate
            });
            return this;
        }

        public INathanApplicationConfigurator WithPostMiddleware(string key, Func<NathanRequestDelegate, NathanRequestDelegate> middleware)
        {
            var postKey = $"Post{key}/{Guid.NewGuid()}";
            Configuration.MiddlewareRegistrations.Add(postKey, new MiddlewareRegistration
            {
                Middleware = middleware,
                Type = MiddlewareType.Delegate
            });
            return this;
        }

        public INathanApplicationConfigurator WithPreMiddleware<TNathanMiddleware>(string key, params object[] parameters) where TNathanMiddleware : INathanMiddleware
        {
            var preKey = $"Pre{key}/{Guid.NewGuid()}";
            Configuration.MiddlewareRegistrations.Add(preKey, new MiddlewareRegistration
            {
                Middleware = typeof(TNathanMiddleware),
                Type = MiddlewareType.Class,
                Parameters = parameters
            });
            return this;
        }

        public INathanApplicationConfigurator WithMiddleware<TNathanMiddleware>(string key, params object[] parameters) where TNathanMiddleware : INathanMiddleware
        {
            Configuration.MiddlewareRegistrations.Add(key, new MiddlewareRegistration
            {
                Middleware = typeof(TNathanMiddleware),
                Type = MiddlewareType.Class,
                Parameters = parameters
            });
            return this;
        }

        public INathanApplicationConfigurator WithPostMiddleware<TNathanMiddleware>(string key, params object[] parameters) where TNathanMiddleware : INathanMiddleware
        {
            var postKey = $"Post{key}/{Guid.NewGuid()}";
            Configuration.MiddlewareRegistrations.Add(postKey, new MiddlewareRegistration
            {
                Middleware = typeof(TNathanMiddleware),
                Type = MiddlewareType.Class,
                Parameters = parameters
            });
            return this;
        }

        internal NathanApplicationConfiguration Configure()
        {
            return Configuration;
        }
    }
}