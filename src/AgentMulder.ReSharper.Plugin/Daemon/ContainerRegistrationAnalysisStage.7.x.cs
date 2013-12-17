using JetBrains.Application.Settings;
using JetBrains.ReSharper.Daemon;
using JetBrains.ReSharper.Psi.CSharp.Tree;

namespace AgentMulder.ReSharper.Plugin.Daemon
{
    // for 7.1
    public partial class ContainerRegistrationAnalysisStage
    {
        protected override IDaemonStageProcess CreateProcess(IDaemonProcess process, IContextBoundSettingsStore settings, DaemonProcessKind processKind, ICSharpFile file)
        {
            return DoCreateProcess(process, settings, processKind);
        }
    }
}