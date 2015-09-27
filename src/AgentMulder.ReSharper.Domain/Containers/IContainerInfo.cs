using System.Collections.Generic;
using AgentMulder.ReSharper.Domain.Patterns;

namespace AgentMulder.ReSharper.Domain.Containers
{
    public interface IContainerInfo
    {
        string ContainerDisplayName { get; }
        IEnumerable<IRegistrationPattern> RegistrationPatterns { get; }
        IEnumerable<string> ContainerQualifiedNames { get; }
    }
}