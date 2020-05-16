using Microsoft.Extensions.DependencyInjection;

namespace Nathan.Abstractions
{
    public interface INathanFeature
    {
        string Key { get;}
        void ConfigureServices(IServiceCollection services);
        void Configure(INathanApplicationConfigurator nathanApplicationConfigurator);
    }
}