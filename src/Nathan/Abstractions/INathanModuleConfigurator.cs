namespace Nathan.Abstractions
{
    public interface INathanModuleConfigurator
    {
        NathanModuleDescriptor ModuleDescriptor { get; }
        INathanModuleConfigurator WithBasePath(string basePath);
        INathanModuleConfigurator WithMetaData(object metaData);
    }
}