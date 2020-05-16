using System;

namespace Nathan.Abstractions
{
    public interface INathanApplicationConfigurator
    {
        IServiceProvider ApplicationServiceProvider { get; }
        NathanApplicationConfiguration Configuration { get; }
        
        INathanApplicationConfigurator WithPreMiddleware(string key,
            Func<NathanRequestDelegate, NathanRequestDelegate> middleware);
        
        INathanApplicationConfigurator WithMiddleware(string key,
            Func<NathanRequestDelegate, NathanRequestDelegate> middleware);
        
        INathanApplicationConfigurator WithPostMiddleware(string key,
            Func<NathanRequestDelegate, NathanRequestDelegate> middleware);
        
        INathanApplicationConfigurator WithPreMiddleware<TNathanMiddleware>(string key, params object[] parameters)
            where TNathanMiddleware: INathanMiddleware;
        
        INathanApplicationConfigurator WithMiddleware<TNathanMiddleware>(string key, params object[] parameters)
            where TNathanMiddleware: INathanMiddleware;
        
        INathanApplicationConfigurator WithPostMiddleware<TNathanMiddleware>(string key, params object[] parameters)
            where TNathanMiddleware: INathanMiddleware;
        
    }
}