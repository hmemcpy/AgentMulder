using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.IO;
using System.Linq;
using System.Reflection;
using AgentMulder.ReSharper.Domain;
using AgentMulder.ReSharper.Domain.Registrations;
using AgentMulder.ReSharper.Domain.Search;
using AgentMulder.ReSharper.Plugin.Highlighting;
using JetBrains.Application.Progress;
using JetBrains.ProjectModel;
using JetBrains.ReSharper.Daemon;
using JetBrains.ReSharper.Daemon.UsageChecking;
using JetBrains.ReSharper.Psi;
using JetBrains.ReSharper.Psi.CSharp;
using JetBrains.ReSharper.Psi.CSharp.Tree;
using JetBrains.ReSharper.Psi.Search;
using JetBrains.ReSharper.Psi.Tree;

namespace AgentMulder.ReSharper.Plugin.Daemon
{
    public class ContainerAnalysisStageProcess : IDaemonStageProcess
    {
        private readonly IDaemonProcess process;
        private readonly CollectUsagesStageProcess usagesStageProcess;
        private readonly SearchDomainFactory searchDomainFactory;

        public ContainerAnalysisStageProcess(IDaemonProcess process, CollectUsagesStageProcess usagesStageProcess, SearchDomainFactory searchDomainFactory)
        {
            this.process = process;
            this.usagesStageProcess = usagesStageProcess;
            this.searchDomainFactory = searchDomainFactory;
        }

        public void Execute(Action<DaemonStageResult> commiter)
        {
            try
            {
                PsiManager manager = PsiManager.GetInstance(process.Solution);
                var file = manager.GetPsiFile(process.SourceFile, CSharpLanguage.Instance) as ICSharpFile;
                if (file == null)
                {
                    return;
                }

                var solutionnAnalyzer = CreateSolutionAnalyzer(process.Solution);
                IEnumerable<IComponentRegistration> componentRegistrations = solutionnAnalyzer.Analyze(process.Solution);

                file.ProcessChildren<ITypeDeclaration>(declaration =>
                {
                    IComponentRegistration registration = componentRegistrations.FirstOrDefault(c => c.IsSatisfiedBy(declaration.DeclaredElement));
                    if (registration != null)
                    {
                        RemovedHighlightings(declaration.DeclaredElement);

                        var highlight = new HighlightingInfo(declaration.GetHighlightingRange(),
                                                             new RegisteredByContainerHighlighting(process.Solution, registration));

                        var result = new DaemonStageResult(new[] { highlight });

                        commiter(result);
                    }
                });
            }
            catch (ProcessCancelledException)
            {
            }
        }

        private SolutionAnalyzer CreateSolutionAnalyzer(ISolution solution)
        {
            var patternSearcher = new PatternSearcher(solution, searchDomainFactory);
            var solutionnAnalyzer = new SolutionAnalyzer(patternSearcher);
            LoadKnownContainersInfo(solutionnAnalyzer);
            return solutionnAnalyzer;
        }

        private void LoadKnownContainersInfo(SolutionAnalyzer solutionnAnalyzer)
        {
            string rootDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

            string containersDirectory = Path.Combine(rootDirectory, "Containers");
            var catalog = new DirectoryCatalog(containersDirectory, "*.dll");
            var container = new CompositionContainer(catalog);
            container.ComposeParts(solutionnAnalyzer);
        }

        private void RemovedHighlightings(ITypeElement typeElement)
        {
            foreach (IConstructor constructor in typeElement.Constructors)
            {
                usagesStageProcess.SetElementState(constructor, UsageState.CANNOT_BE_PROTECTED | UsageState.CANNOT_BE_INTERNAL | UsageState.CANNOT_BE_PRIVATE | UsageState.USED_MASK);
            }
           
            usagesStageProcess.SetElementState(typeElement, UsageState.ACCESSED);
        }

        public IDaemonProcess DaemonProcess
        {
            get { return process; }
        }
    }
}