using System;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.IO;
using System.Linq;
using System.Reflection;
using AgentMulder.ReSharper.Domain;
using AgentMulder.ReSharper.Domain.Registrations;
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
            PsiManager manager = PsiManager.GetInstance(process.Solution);
            var file = manager.GetPsiFile(process.SourceFile, CSharpLanguage.Instance) as ICSharpFile;
            if (file == null)
            {
                return;
            }

            var solutionnAnalyzer = new SolutionAnalyzer(searchDomainFactory);
            LoadKnownContainersInfo(solutionnAnalyzer);
            solutionnAnalyzer.Analyze(process.Solution);

            file.ProcessChildren<ITypeDeclaration>(declaration =>
            {
                IComponentRegistration registration = solutionnAnalyzer.ComponentRegistrations.FirstOrDefault(r => r.IsSatisfiedBy(declaration.DeclaredElement));
                if (registration != null)
                {
                    RemovedHighlightings(declaration.DeclaredElement);

                    var highlight = new HighlightingInfo(declaration.GetDocumentRange(),
                                                         new RegisteredByContainerHighlighting(new ContainerInfo("Castle Windsor", process.SourceFile.DisplayName)));

                    var result = new DaemonStageResult(new[] { highlight });

                    commiter(result);
                }
            });
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
        }

        public IDaemonProcess DaemonProcess
        {
            get { return process; }
        }
    }

    public class ContainerInfo
    {
        public string ContainerName { get; set; }
        public string DisplayName { get; set; }

        public ContainerInfo(string castleWindsor, string displayName)
        {
            ContainerName = castleWindsor;
            DisplayName = displayName;
        }
    }
}