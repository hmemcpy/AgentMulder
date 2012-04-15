using System.Collections.Generic;

namespace AgentMulder.Core
{
    public interface IContainerInfo
    {
        string Name { get; }
        IEnumerable<string> AssemblyNames { get; }
        IRegistrationParser RegistrationParser { get; }
        bool HasContainerReference(IEnumerable<string> projectAssemblyReferences);
    }
}