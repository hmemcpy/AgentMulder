using System.Collections.Generic;
using ICSharpCode.NRefactory.CSharp;
using ICSharpCode.NRefactory.TypeSystem;

namespace AgentMulder.Core.TypeSystem
{
    public interface IProject
    {
        ISolution Solution { get; }
        ICompilation Compilation { get; }
        IProjectContent ProjectContent { get; }
        IEnumerable<IFile> Files { get; }
        IEnumerable<IAssembly> ResolvedAssemblyReferences { get; }

        string FileName { get; }
        string Title { get; }
    }
}