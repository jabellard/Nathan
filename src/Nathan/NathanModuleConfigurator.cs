
using Nathan.Abstractions;

namespace Nathan
{
    public class NathanModuleConfigurator: INathanModuleConfigurator
    {
        public NathanModuleDescriptor ModuleDescriptor { get; }

        public NathanModuleConfigurator(NathanModuleDescriptor moduleDescriptor)
        {
            ModuleDescriptor = moduleDescriptor;
        }

        public INathanModuleConfigurator WithBasePath(string basePath)
        {
            ModuleDescriptor.BasePath = basePath;
            return this;
        }
        
        public INathanModuleConfigurator WithMetaData(object metaData)
        {
            ModuleDescriptor.MetaData.Add(metaData);
            return this;
        }
    }
}