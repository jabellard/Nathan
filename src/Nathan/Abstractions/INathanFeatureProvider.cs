using System.Collections.Generic;

namespace Nathan.Abstractions
{
    public interface INathanFeatureProvider
    {
        IEnumerable<INathanFeature> GetFeatures();
    }
}