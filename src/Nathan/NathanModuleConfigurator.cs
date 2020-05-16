
namespace Nathan
{
    public class NathanModuleConfigurator
    {
        public NathanModuleDescriptor ModuleDescriptor { get; }

        public NathanModuleConfigurator(NathanModuleDescriptor moduleDescriptor)
        {
            ModuleDescriptor = moduleDescriptor;
        }
        
        public NathanModuleConfigurator WithMetaData(object metaData)
        {
            throw new System.NotImplementedException();
        }
    }
}