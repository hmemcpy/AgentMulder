using System.Collections.Generic;
using AgentMulder.ReSharper.Domain.Search;

namespace AgentMulder.ReSharper.Domain.Containers
{
    public interface IContainerInfo
    {
        string Name { get; }
        IEnumerable<string> ContainerAssemblyNames { get; }
        IEnumerable<IComponentRegistrationPattern> RegistrationPatterns { get; }
        bool HasContainerReference(IEnumerable<string> projectAssemblyReferences);
    }
}