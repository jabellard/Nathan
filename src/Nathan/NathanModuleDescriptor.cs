using System;
using System.Collections.Generic;

namespace Nathan
{
    public class NathanModuleDescriptor
    {
        public Type ModuleType { get; }
        public string BasePath { get; set; } 
        public IList<object> MetaData { get; }
        public IDictionary<string, NathanHandlerDescriptor> HandlerDescriptors { get; }

        public NathanModuleDescriptor(Type moduleType)
        {
            ModuleType = moduleType;
            BasePath = string.Empty;
            MetaData = new List<object>();
            HandlerDescriptors = new Dictionary<string, NathanHandlerDescriptor>();
        }

        public void AddHandlerDescriptor(string method, string pathTemplate, NathanRequestDelegate requestDelegate,
            DeferredEndPointConventionBuilder endPointConventionBuilder)
        {
            var prefixedPathTemplate = $"{BasePath}/{pathTemplate}";
            var handlerDescriptor = new NathanHandlerDescriptor(method, prefixedPathTemplate, requestDelegate, endPointConventionBuilder, this);
            HandlerDescriptors.Add(handlerDescriptor.Key, handlerDescriptor);
        }
    }
}