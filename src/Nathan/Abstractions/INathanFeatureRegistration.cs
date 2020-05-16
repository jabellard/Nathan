using System;

namespace Nathan.Abstractions
{
    public interface INathanFeatureRegistration
    {
        INathanFeature Feature { get;}
        DateTime Timestamp { get;}
    }
}