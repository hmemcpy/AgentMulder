using System;
using JetBrains.Application.Settings;
using JetBrains.ReSharper.Daemon;
using JetBrains.ReSharper.Daemon.UsageChecking;
using JetBrains.ReSharper.Psi;
using JetBrains.ReSharper.Psi.CSharp;

namespace AgentMulder.ReSharper.Plugin.Daemon
{
    [DaemonStage(StagesBefore = new[] { typeof(LanguageSpecificDaemonStage) })]
    public class ContainerAnalysisDaemonStage : IDaemonStage
    {
        public IDaemonStageProcess CreateProcess(IDaemonProcess process, IContextBoundSettingsStore settings, DaemonProcessKind processKind)
        {
            var usagesStageProcess = process.GetStageProcess<CollectUsagesStageProcess>();
            
            return usagesStageProcess == null ? null : new ContainerAnalysisStageProcess(process, usagesStageProcess);
        }

        public ErrorStripeRequest NeedsErrorStripe(IPsiSourceFile sourceFile, IContextBoundSettingsStore settingsStore)
        {
            return ErrorStripeRequest.NONE;
        }
    }

}