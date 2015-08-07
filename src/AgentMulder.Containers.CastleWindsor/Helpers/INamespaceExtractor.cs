using System;
using AgentMulder.ReSharper.Domain.Elements;
using JetBrains.ReSharper.Psi;

namespace AgentMulder.Containers.CastleWindsor.Helpers
{
    public interface INamespaceExtractor
    {
        INamespace GetNamespace<T>(T element, out bool includeSubnamespaces);
    }
}