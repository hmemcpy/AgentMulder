using ICSharpCode.NRefactory.CSharp;
using ICSharpCode.NRefactory.Editor;

namespace AgentMulder.Core.ProjectModel
{
    public interface ICodeFile
    {
        CompilationUnit CompilationUnit { get; }
        ITextSource Content { get; }
        string FileName { get; }
    }
}