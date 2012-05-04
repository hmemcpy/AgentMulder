using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.Linq;
using AgentMulder.ReSharper.Domain;
using AgentMulder.ReSharper.Domain.Registrations;
using AgentMulder.ReSharper.Domain.Search;
using AgentMulder.ReSharper.Plugin.Highlighting;
using JetBrains.Application.Progress;
using JetBrains.ProjectModel;
using JetBrains.ReSharper.Daemon;
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
        private readonly ITypeUsageManager usageManager;
        private readonly SearchDomainFactory searchDomainFactory;
        private readonly string containersRootDirectory;
        private readonly SolutionAnalyzer solutionnAnalyzer;

        public ContainerAnalysisStageProcess(IDaemonProcess process, 
                                             ITypeUsageManager usageManager, 
                                             SearchDomainFactory searchDomainFactory, 
                                             string containersRootDirectory)
        {
            this.process = process;
            this.usageManager = usageManager;
            this.searchDomainFactory = searchDomainFactory;
            this.containersRootDirectory = containersRootDirectory;
            
            solutionnAnalyzer = CreateSolutionAnalyzer(process.Solution);
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
                
                IEnumerable<IComponentRegistration> componentRegistrations = solutionnAnalyzer.Analyze(process.Solution);

                file.ProcessChildren<ITypeDeclaration>(declaration =>
                {
                    IComponentRegistration registration = componentRegistrations.FirstOrDefault(c => c.IsSatisfiedBy(declaration.DeclaredElement));
                    if (registration != null)
                    {
                        usageManager.MarkTypeAsUsed(declaration.DeclaredElement);

                        var highlighting = new HighlightingInfo(declaration.GetNameDocumentRange(), 
                            new RegisteredByContainerHighlighting(process.Solution, registration));

                        var result = new DaemonStageResult(new[] { highlighting });

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
            var result = new SolutionAnalyzer(patternSearcher);
            try
            {
                var catalog = new DirectoryCatalog(containersRootDirectory, "*.dll");
                var container = new CompositionContainer(catalog);
                container.ComposeParts(result);
            }
            catch
            {
            }

            return result;
        }

        public IDaemonProcess DaemonProcess
        {
            get { return process; }
        }
    }
}