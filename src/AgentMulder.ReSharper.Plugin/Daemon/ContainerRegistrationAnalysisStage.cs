using System;
using AgentMulder.ReSharper.Plugin.Components;
using JetBrains.Application.Settings;
using JetBrains.ReSharper.Daemon;
using JetBrains.ReSharper.Daemon.CSharp.Stages;
using JetBrains.ReSharper.Daemon.UsageChecking;

namespace AgentMulder.ReSharper.Plugin.Daemon
{
    [DaemonStage(StagesBefore = new[] { typeof(LanguageSpecificDaemonStage) })]
    public partial class ContainerRegistrationAnalysisStage : CSharpDaemonStageBase
    {
        private readonly IPatternManager patternManager;

        public ContainerRegistrationAnalysisStage(IPatternManager patternManager)
        {
            this.patternManager = patternManager;
        }

        private IDaemonStageProcess DoCreateProcess(IDaemonProcess process, IContextBoundSettingsStore settings, DaemonProcessKind processKind)
        {
            if (!IsSupported(process.SourceFile))
            {
                return null;
            }

            if (processKind != DaemonProcessKind.VISIBLE_DOCUMENT)
            {
                return null;
            }

            var collectUsagesStageProcess = process.GetStageProcess<CollectUsagesStageProcess>();
            var typeUsageManager = new TypeUsageManager(collectUsagesStageProcess);

            return new ContainerRegistrationAnalysisStageProcess(process, settings, typeUsageManager, patternManager);
        }
    }
}