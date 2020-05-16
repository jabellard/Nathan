using System;
using Nathan.Abstractions;

namespace Nathan
{
    public class NathanFeatureRegistration: INathanFeatureRegistration
    {
        public INathanFeature Feature { get; set; }
        public DateTime Timestamp { get; set; }
    }
}