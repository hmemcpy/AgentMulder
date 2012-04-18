using System.Collections.Generic;
using ICSharpCode.NRefactory.TypeSystem;

namespace AgentMulder.Core.ProjectModel
{
    public interface ISolution
    {
        ISolutionSnapshot SolutionSnapshot { get; }
        IEnumerable<IProject> Projects { get; }
    }
}