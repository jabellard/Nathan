using System;
using Microsoft.AspNetCore.Builder;

namespace Nathan
{
    public class NathanEndPointConventionBuilder: IEndpointConventionBuilder
    {
        public void Add(Action<EndpointBuilder> convention)
        {
            throw new NotImplementedException();
        }
    }
}