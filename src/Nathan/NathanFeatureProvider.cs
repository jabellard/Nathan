using System.Collections.Generic;
using System.Linq;
using Nathan.Abstractions;

namespace Nathan
{
    public class NathanFeatureProvider: INathanFeatureProvider
    {
        private readonly IEnumerable<INathanFeatureRegistration> _nathanFeatureRegistrations;

        public NathanFeatureProvider(IEnumerable<INathanFeatureRegistration> nathanFeatureRegistrations)
        {
            _nathanFeatureRegistrations = nathanFeatureRegistrations;
        }
        
        public IEnumerable<INathanFeature> GetFeatures()
        {
            return _nathanFeatureRegistrations
                .OrderBy(fr => fr.Timestamp)
                .Select(fr => fr.Feature);
        }
    }
}