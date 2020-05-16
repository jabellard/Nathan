using Nathan.Abstractions;

namespace Nathan.Extensions
{
    public static class NathanApplicationBuilderExtensions
    {
        public static INathanApplicationBuilder ConfigurePipeline(
            this INathanApplicationBuilder nathanApplicationBuilder,
            INathanApplicationConfiguration nathanApplicationConfiguration)
        {
            return nathanApplicationBuilder;
        }
    }
}