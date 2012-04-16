using System.Collections.Generic;
using ICSharpCode.NRefactory.CSharp;
using ICSharpCode.NRefactory.TypeSystem;

namespace AgentMulder.TypeSystem.ProjectModel
{
    public interface ICSharpProject
    {
        IEnumerable<IAssembly> AssemblyReferences { get; }
        IEnumerable<IFile> Files { get; }
        ICompilation Compilation { get; }
        string Name { get; }

        CSharpParser CreateParser();
    }
}