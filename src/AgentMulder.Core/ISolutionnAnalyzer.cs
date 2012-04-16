using System.Collections.Generic;
using AgentMulder.Core.TypeSystem;

namespace AgentMulder.Core
{
    public interface ISolutionnAnalyzer
    {
        IEnumerable<IContainerInfo> KnownContainers { get; }

        IEnumerable<Registration> RegisteredTypes { get; }

        void Analyze(ISolution solution);
    }
}