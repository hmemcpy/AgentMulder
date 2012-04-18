using System.Collections.Generic;
using ICSharpCode.NRefactory.TypeSystem;

namespace AgentMulder.Core.ProjectModel
{
    public interface IProject
    {
        ISolution Solution { get; }
        ICompilation Compilation { get; }
        IProjectContent ProjectContent { get; }
        IEnumerable<ICodeFile> Files { get; }
        IEnumerable<IAssembly> ResolvedAssemblyReferences { get; }

        string FileName { get; }
        string Title { get; }
    }
}