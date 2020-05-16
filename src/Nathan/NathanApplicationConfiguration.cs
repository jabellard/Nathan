using System.Collections.Generic;

namespace Nathan
{
    public class NathanApplicationConfiguration
    {
        public IDictionary<string, MiddlewareRegistration> MiddlewareRegistrations { get; }

        public NathanApplicationConfiguration()
        {
            MiddlewareRegistrations = new Dictionary<string, MiddlewareRegistration>();
        }
    }
}