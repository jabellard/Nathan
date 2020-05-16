using System.Collections.Generic;
using Nathan.Abstractions;

namespace Nathan
{
    public class NathanApplicationConfiguration: INathanApplicationConfiguration
    {
        public IDictionary<string, MiddlewareRegistration> MiddlewareRegistrations { get; }
    }
}