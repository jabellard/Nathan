using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Builder;

namespace Nathan
{
    public class DeferredEndPointConventionBuilder: IEndpointConventionBuilder
    {
        private readonly List<Action<EndpointBuilder>> _deferredConventions;
        public DeferredEndPointConventionBuilder()
        {
            _deferredConventions = new List<Action<EndpointBuilder>>();
        }
        
        public void Add(Action<EndpointBuilder> convention)
        {
            _deferredConventions.Add(convention);
        }

        public void Execute(IEndpointConventionBuilder endpointConventionBuilder)
        {
            foreach (var deferredConvention in _deferredConventions)
                endpointConventionBuilder.Add(deferredConvention);
        }
    }
}