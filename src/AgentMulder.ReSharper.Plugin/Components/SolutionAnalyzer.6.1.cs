using JetBrains.ReSharper.Psi;
using JetBrains.ReSharper.Psi.CSharp;
using JetBrains.ReSharper.Psi.CSharp.Tree;

namespace AgentMulder.ReSharper.Plugin.Components
{
    public partial class SolutionAnalyzer
    {
        private ICSharpFile GetCSharpFile(IPsiSourceFile sourceFile)
        {
            return sourceFile.GetPsiFile(CSharpLanguage.Instance) as ICSharpFile as ICSharpFile;
        }
    }
}