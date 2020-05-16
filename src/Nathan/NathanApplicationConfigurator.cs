using System;
using Nathan.Abstractions;

namespace Nathan
{
    public class NathanApplicationConfigurator: INathanApplicationConfigurator
    {
        public IServiceProvider ApplicationServiceProvider { get; }
        public INathanApplicationConfiguration Configuration { get; }

        public NathanApplicationConfigurator(IServiceProvider applicationServiceProvider)
        {
            ApplicationServiceProvider = applicationServiceProvider;
            Configuration = new NathanApplicationConfiguration();
        }
        public INathanApplicationConfigurator WithPreMiddleware(string key, Func<NathanRequestDelegate, NathanRequestDelegate> middleware)
        {
            throw new NotImplementedException();
        }

        public INathanApplicationConfigurator WithMiddleware(string key, Func<NathanRequestDelegate, NathanRequestDelegate> middleware)
        {
            throw new NotImplementedException();
        }

        public INathanApplicationConfigurator WithPostMiddleware(string key, Func<NathanRequestDelegate, NathanRequestDelegate> middleware)
        {
            throw new NotImplementedException();
        }

        public INathanApplicationConfigurator WithPreMiddleware<TNathanMiddleware>(string key, params object[] parameters) where TNathanMiddleware : INathanMiddleware
        {
            throw new NotImplementedException();
        }

        public INathanApplicationConfigurator WithMiddleware<TNathanMiddleware>(string key, params object[] parameters) where TNathanMiddleware : INathanMiddleware
        {
            throw new NotImplementedException();
        }

        public INathanApplicationConfigurator WithPostMiddleware<TNathanMiddleware>(string key, params object[] parameters) where TNathanMiddleware : INathanMiddleware
        {
            throw new NotImplementedException();
        }

        public INathanApplicationConfiguration Configure()
        {
            throw new NotImplementedException();
        }
    }
}