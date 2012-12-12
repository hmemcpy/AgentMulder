using JetBrains.ReSharper.Psi;
using JetBrains.ReSharper.Psi.Tree;

namespace AgentMulder.ReSharper.Plugin.Daemon
{
    public partial class ContainerRegistrationAnalysisStageProcess
    {
        private static IFile GetPsiFile(IPsiSourceFile psiSourceFile)
        {
            return psiSourceFile.GetPsiFile(psiSourceFile.PrimaryPsiLanguage);
        }
    }
}