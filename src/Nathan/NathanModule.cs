using Nathan.Abstractions;

namespace Nathan
{
    public class NathanModule
    {
        public NathanModuleDescriptor ModuleDescriptor => new NathanModuleDescriptor(GetType());
    }
}