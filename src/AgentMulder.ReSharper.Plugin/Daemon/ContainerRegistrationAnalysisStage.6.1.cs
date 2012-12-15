using JetBrains.Application.Settings;
using JetBrains.ReSharper.Daemon;

namespace AgentMulder.ReSharper.Plugin.Daemon
{
    public partial class ContainerRegistrationAnalysisStage
    {
        public override IDaemonStageProcess CreateProcess(IDaemonProcess process, IContextBoundSettingsStore settings, DaemonProcessKind processKind)
        {
            return DoCreateProcess(process, settings, processKind);
        }
    }
}