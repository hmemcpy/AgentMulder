using System.Collections.Generic;
using AgentMulder.Core.NRefactory;
using JetBrains.ProjectModel;

namespace AgentMulder.Core
{
    public interface ISolutionnAnalyzer
    {
        IEnumerable<IContainerInfo> KnownContainers { get; }

        IEnumerable<Registration> RegisteredTypes { get; }

        void Analyze(Solution solution);
    }
}