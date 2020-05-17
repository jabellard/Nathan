using Nathan.Abstractions;

namespace Nathan
{
    public class NathanHandlerDescriptor: INathanHandlerDescriptorMetaData
    {
        public string Key => $"{Method}-{PathTemplate}";
        public string Method { get; }
        public string PathTemplate { get; }
        public NathanRequestDelegate RequestDelegate { get; }
        public DeferredEndPointConventionBuilder EndPointConventionBuilder { get; }
        public NathanModuleDescriptor ParentModuleDescriptor { get; }

        public NathanHandlerDescriptor(string method, string pathTemplate, NathanRequestDelegate requestDelegate,
            DeferredEndPointConventionBuilder endPointConventionBuilder, NathanModuleDescriptor parentModuleDescriptor)
        {
            Method = method;
            PathTemplate = pathTemplate;
            RequestDelegate = requestDelegate;
            EndPointConventionBuilder = endPointConventionBuilder;
            ParentModuleDescriptor = parentModuleDescriptor;
        }
    }
}