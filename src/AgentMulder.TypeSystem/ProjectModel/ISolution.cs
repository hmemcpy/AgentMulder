using System.Collections;
using System.Collections.Generic;
using ICSharpCode.NRefactory.TypeSystem;

namespace AgentMulder.TypeSystem.ProjectModel
{
    public interface ISolution
    {
        string Name { get; }
        IEnumerable<ICSharpProject> Projects { get; }
        ISolutionSnapshot SolutionSnapshot { get; }
    }
}