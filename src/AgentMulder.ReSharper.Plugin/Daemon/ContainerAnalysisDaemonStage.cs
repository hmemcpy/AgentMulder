using System;
using JetBrains.Application.Settings;
using JetBrains.ReSharper.Daemon;
using JetBrains.ReSharper.Daemon.CSharp.Stages;
using JetBrains.ReSharper.Daemon.UsageChecking;

namespace AgentMulder.ReSharper.Plugin.Daemon
{
    [DaemonStage(GlobalAnalysisStage = true, 
                 StagesBefore = new[] { typeof(CollectUsagesStage) }, 
                 StagesAfter = new[] { typeof(LanguageSpecificDaemonStage) })]
    public class ContainerAnalysisDaemonStage : CSharpDaemonStageBase
    {
        public override IDaemonStageProcess CreateProcess(IDaemonProcess process, IContextBoundSettingsStore settings, DaemonProcessKind processKind)
        {
            return new ContainerAnalysisStageProcess(process);
        }
    }
}