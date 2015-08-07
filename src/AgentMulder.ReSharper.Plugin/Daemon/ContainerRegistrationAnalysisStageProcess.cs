using System;
using System.Collections.Generic;
using System.Linq;
using AgentMulder.ReSharper.Plugin.Components;
using AgentMulder.ReSharper.Plugin.Highlighting;
using JetBrains.Application.Settings;
using JetBrains.ReSharper.Daemon;
using JetBrains.ReSharper.Daemon.Stages;
using JetBrains.ReSharper.Psi;
using JetBrains.ReSharper.Psi.Tree;
#if SDK90
using JetBrains.ReSharper.Feature.Services.Daemon;
#endif

namespace AgentMulder.ReSharper.Plugin.Daemon
{
    public partial class ContainerRegistrationAnalysisStageProcess : IDaemonStageProcess
    {
        private readonly IDaemonProcess process;
        private readonly IContextBoundSettingsStore settingsStore;
        private readonly IPatternManager patternManager;

        public ContainerRegistrationAnalysisStageProcess(IDaemonProcess process, IContextBoundSettingsStore settingsStore, IPatternManager patternManager)
        {
            this.process = process;
            this.settingsStore = settingsStore;
            this.patternManager = patternManager;
        }

        public void Execute(Action<DaemonStageResult> commiter)
        {
            var consumer = new DefaultHighlightingConsumer(this, settingsStore);

            foreach (IFile psiFile in EnumeratePsiFiles())
            {
                ProcessFile(psiFile, consumer);
            }

            commiter(new DaemonStageResult(consumer.Highlightings));
        }

        private IEnumerable<IFile> EnumeratePsiFiles()
        {
            return DaemonProcess.SourceFile.EnumerateDominantPsiFiles();
        }

        public IDaemonProcess DaemonProcess
        {
            get { return process; }
        }
    }
}