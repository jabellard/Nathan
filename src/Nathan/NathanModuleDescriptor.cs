using System.Collections.Generic;

namespace Nathan
{
    public class NathanModuleDescriptor
    {
        public string BasePath { get; set; } = string.Empty;
        public List<NathanHandlerDescriptor> HandlerDescriptors { get; set; }
    }
}