using System;
using System.Collections.Generic;

namespace Nathan
{
    public class NathanModuleDescriptor
    {
        public Type ModuleType { get; }
        public string BasePath { get; set; } 
        public IList<object> MetaData { get; }
        public List<NathanHandlerDescriptor> HandlerDescriptors { get; }

        public NathanModuleDescriptor(Type moduleType)
        {
            ModuleType = moduleType;
            BasePath = string.Empty;
            MetaData = new List<object>();
            HandlerDescriptors = new List<NathanHandlerDescriptor>();
        }

        public void AddHandlerDescriptor(IEnumerable<string> methods, string pathTemplate,
            DeferredEndPointConventionBuilder endPointConventionBuilder)
        {
            var prefixedPathTemplate = $"{BasePath}/{pathTemplate}";
            var handlerDescriptor = new NathanHandlerDescriptor(methods, prefixedPathTemplate, endPointConventionBuilder, this);
            HandlerDescriptors.Add(handlerDescriptor);
        }
    }
}