using ICSharpCode.NRefactory.CSharp;
using ICSharpCode.NRefactory.Editor;

namespace AgentMulder.Core.TypeSystem
{
    public interface IFile
    {
        CompilationUnit CompilationUnit { get; }
        ITextSource Content { get; }
        string FileName { get; }
    }
}