using System;
using Nathan.Abstractions;

namespace Nathan
{
    public class NathanApplicationBuilder: INathanApplicationBuilder
    {
        public IServiceProvider ApplicationServiceProvider { get; }

        public NathanApplicationBuilder(IServiceProvider applicationServiceProvider)
        {
            ApplicationServiceProvider = applicationServiceProvider;
        }
        public INathanApplicationBuilder UseMiddleware<TNathanMiddleware>(params object[] parameters) where TNathanMiddleware : INathanMiddleware
        {
            throw new NotImplementedException();
        }

        public INathanApplicationBuilder UseMiddleware(Type middlewareType, params object[] parameters)
        {
            throw new NotImplementedException();
        }

        public INathanApplicationBuilder Use(Func<NathanRequestDelegate, NathanRequestDelegate> middleware)
        {
            throw new NotImplementedException();
        }

        public NathanRequestDelegate Build()
        {
            throw new NotImplementedException();
        }
    }
}