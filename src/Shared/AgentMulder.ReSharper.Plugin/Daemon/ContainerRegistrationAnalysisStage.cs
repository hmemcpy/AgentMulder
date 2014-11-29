using System;
using AgentMulder.ReSharper.Plugin.Components;
using JetBrains.Application.Settings;
using JetBrains.ReSharper.Daemon;
using JetBrains.ReSharper.Daemon.CSharp.Stages;
using JetBrains.ReSharper.Daemon.UsageChecking;
using JetBrains.ReSharper.Psi.CSharp.Tree;
#if SDK90
using JetBrains.ReSharper.Feature.Services.CSharp.Daemon;
using JetBrains.ReSharper.Feature.Services.Daemon;
#endif

namespace AgentMulder.ReSharper.Plugin.Daemon
{
    [DaemonStage(StagesBefore = new[] { typeof(LanguageSpecificDaemonStage) })]
    public class ContainerRegistrationAnalysisStage : CSharpDaemonStageBase
    {
        private readonly IPatternManager patternManager;

        public ContainerRegistrationAnalysisStage(IPatternManager patternManager)
        {
            this.patternManager = patternManager;
        }

        protected override IDaemonStageProcess CreateProcess(IDaemonProcess process, IContextBoundSettingsStore settings, DaemonProcessKind processKind, ICSharpFile file)
        {
            return DoCreateProcess(process, settings, processKind);
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