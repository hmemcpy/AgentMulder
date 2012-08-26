using System;
using System.Collections.Generic;
using System.Linq;
using AgentMulder.ReSharper.Plugin.Components;
using AgentMulder.ReSharper.Plugin.Highlighting;
using JetBrains.Application.Progress;
using JetBrains.Application.Settings;
using JetBrains.ReSharper.Daemon;
using JetBrains.ReSharper.Daemon.Stages;
using JetBrains.ReSharper.Psi;
using JetBrains.ReSharper.Psi.Tree;

namespace AgentMulder.ReSharper.Plugin.Daemon
{
    public class ContainerRegistrationAnalysisStageProcess : IDaemonStageProcess
    {
        private readonly IDaemonProcess process;
        private readonly IContextBoundSettingsStore settingsStore;
        private readonly ITypeUsageManager typeUsageManager;
        private readonly IEnumerable<RegistrationInfo> cachedComponentRegistrations;

        public ContainerRegistrationAnalysisStageProcess(IDaemonProcess process, IContextBoundSettingsStore settingsStore, ITypeUsageManager typeUsageManager, SolutionAnalyzer solutionAnalyzer)
        {
            this.process = process;
            this.settingsStore = settingsStore;
            this.typeUsageManager = typeUsageManager;

            cachedComponentRegistrations = solutionAnalyzer.Analyze().ToList();
        }

        public void Execute(Action<DaemonStageResult> commiter)
        {
            var consumer = new DefaultHighlightingConsumer(this, settingsStore);

            foreach (IFile psiFile in DaemonProcess.SourceFile.EnumeratePsiFiles())
            {
                psiFile.ProcessChildren<ITypeDeclaration>(declaration =>
                {
                    if (declaration.DeclaredElement == null) // type is not (yet) declared
                    {
                        return;
                    }
                    
                    RegistrationInfo registrationInfo = cachedComponentRegistrations.FirstOrDefault(c => c.Registration.IsSatisfiedBy(declaration.DeclaredElement));
                    if (registrationInfo != null)
                    {
                        consumer.AddHighlighting(new RegisteredByContainerHighlighting(registrationInfo),
                                                 declaration.GetNameDocumentRange(),
                                                 registrationInfo.GetRegistrationFile());
                        
                        typeUsageManager.MarkTypeAsUsed(declaration);
                    }
                });
            }

            commiter(new DaemonStageResult(consumer.Highlightings));
        }

        public IDaemonProcess DaemonProcess
        {
            get { return process; }
        }
    }
}