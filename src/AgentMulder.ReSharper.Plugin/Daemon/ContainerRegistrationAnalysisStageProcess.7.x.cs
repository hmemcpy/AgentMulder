using JetBrains.ReSharper.Psi;
using JetBrains.ReSharper.Psi.Tree;

namespace AgentMulder.ReSharper.Plugin.Daemon
{
    public partial class ContainerRegistrationAnalysisStageProcess
    {
        private IFile GetPsiFile(IPsiSourceFile psiSourceFile)
        {
            return psiSourceFile.GetTheOnlyPsiFile(psiSourceFile.PrimaryPsiLanguage);
        }
    }
}