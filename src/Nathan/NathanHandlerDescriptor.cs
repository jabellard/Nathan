using System.Collections.Generic;
using System.Linq;
using Nathan.Abstractions;

namespace Nathan
{
    public class NathanHandlerDescriptor: INathanHandlerDescriptorMetaData
    {
        public string[] Methods { get; }
        public string PathTemplate { get; }
        public DeferredEndPointConventionBuilder EndPointConventionBuilder { get; }
        public NathanModuleDescriptor ParentModuleDescriptor { get; }

        public NathanHandlerDescriptor(IEnumerable<string> methods, string pathTemplate,
            DeferredEndPointConventionBuilder endPointConventionBuilder, NathanModuleDescriptor parentModuleDescriptor)
        {
            Methods = methods.ToArray();
            PathTemplate = pathTemplate;
            EndPointConventionBuilder = endPointConventionBuilder;
            ParentModuleDescriptor = parentModuleDescriptor;
        }
    }
}