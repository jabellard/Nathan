using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Builder;

namespace Nathan
{
    public class AggregatedEndpointConventionBuilder: IEndpointConventionBuilder
    {
        private readonly IEnumerable<IEndpointConventionBuilder> _endpointConventionBuilders;

        public AggregatedEndpointConventionBuilder(IEnumerable<IEndpointConventionBuilder> endpointConventionBuilders)
        {
            _endpointConventionBuilders = endpointConventionBuilders;
        }

        public void Add(Action<EndpointBuilder> convention)
        {
            foreach (var endpointConventionBuilder in _endpointConventionBuilders)
                endpointConventionBuilder.Add(convention);
        }
    }
}