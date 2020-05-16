using System.Collections.Generic;

namespace Nathan.Abstractions
{
    public interface INathanModuleProvider
    {
        IEnumerable<NathanModule> GetModules();
    }
}