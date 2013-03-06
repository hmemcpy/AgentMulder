using JetBrains.DocumentModel;
using JetBrains.ReSharper.Psi;
using JetBrains.ReSharper.Psi.CSharp;
using JetBrains.ReSharper.Psi.CSharp.Tree;
using JetBrains.ReSharper.Psi.Files;

namespace AgentMulder.ReSharper.Plugin.Components
{
    public partial class SolutionAnalyzer
    {
        private ICSharpFile GetCSharpFile(IPsiSourceFile sourceFile)
        {
            return sourceFile.GetPsiFile<CSharpLanguage>(new DocumentRange(sourceFile.Document, sourceFile.Document.DocumentRange)) as ICSharpFile;
        }
    }
}