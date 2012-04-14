using System;
using System.Linq;
using AgentMulder.Core;
using ICSharpCode.NRefactory.ConsistencyCheck;
using JetBrains.Application.Settings;
using JetBrains.ProjectModel;
using JetBrains.ReSharper.Daemon;
using JetBrains.ReSharper.Daemon.CSharp.Stages;
using JetBrains.ReSharper.Daemon.UsageChecking;
using JetBrains.ReSharper.Psi;
using JetBrains.ReSharper.Psi.CSharp;
using JetBrains.ReSharper.Psi.CSharp.Tree;
using JetBrains.ReSharper.Psi.Tree;

namespace AgentMulder.ReSharper.Plugin
{
    [DaemonStage(StagesBefore = new[] { typeof(CollectUsagesStage) }, StagesAfter = new[] { typeof(LanguageSpecificDaemonStage) })]
    public class ContainerAnalysisDaemonStage : CSharpDaemonStageBase
    {
        public override IDaemonStageProcess CreateProcess(IDaemonProcess process, IContextBoundSettingsStore settings, DaemonProcessKind processKind)
        {
            return new ContainerAnalysisStageProcess(process);
        }
    }

    public class ContainerAnalysisStageProcess : IDaemonStageProcess
    {
        private readonly IDaemonProcess process;

        public ContainerAnalysisStageProcess(IDaemonProcess process)
        {
            this.process = process;
        }

        public void Execute(Action<DaemonStageResult> commiter)
        {
            PsiManager manager = PsiManager.GetInstance(process.Solution);
            var file = manager.GetPsiFile(process.SourceFile, CSharpLanguage.Instance) as ICSharpFile;
            if (file == null)
            {
                return;
            }

            IProject proj =
                process.Solution.GetAllProjects().SingleOrDefault(
                    p => p.GetAssemblyReferences().Any(reference => reference.Name == "Castle.Windsor"));

            if (proj == null)
            {
                return;
            }

            Solution solution = new Solution(process.Solution.SolutionFilePath.FullPath);
            CSharpProject project =
                solution.Projects.Find(sharpProject => sharpProject.FileName == proj.ProjectFileLocation.FullPath);
            WindsorAnalyzer analyzer = new WindsorAnalyzer();
            analyzer.Analyze(project);

            CollectUsagesStageProcess usages = process.GetStageProcess<CollectUsagesStageProcess>();

            file.ProcessChildren<ITypeDeclaration>(declaration => 
            {
                if (analyzer.RegisteredTypes.Contains(declaration.CLRName))
                {
                    var highlight = new HighlightingInfo(declaration.GetDocumentRange(),
                                                         new RegisteredByContainerHighlighting(new ContainerInfo("Castle Windsor", process.SourceFile.DisplayName)));

                    var result = new DaemonStageResult(new[] { highlight });

                    commiter(result);
                }

            });


        }

        public IDaemonProcess DaemonProcess
        {
            get { return process; }
        }
    }

    public class ContainerInfo
    {
        public string ContainerName { get; set; }
        public string LocationName { get; set; }

        public ContainerInfo(string containerName, string locationName)
        {
            ContainerName = containerName;
            LocationName = locationName;
        }
    }
}