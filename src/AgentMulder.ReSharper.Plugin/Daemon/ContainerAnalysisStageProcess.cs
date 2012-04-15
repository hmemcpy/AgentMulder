using System;
using System.Linq;
using AgentMulder.Core;
using AgentMulder.Core.NRefactory;
using JetBrains.ProjectModel;
using JetBrains.ReSharper.Daemon;
using JetBrains.ReSharper.Daemon.UsageChecking;
using JetBrains.ReSharper.Psi;
using JetBrains.ReSharper.Psi.CSharp;
using JetBrains.ReSharper.Psi.CSharp.Tree;
using JetBrains.ReSharper.Psi.Tree;

namespace AgentMulder.ReSharper.Plugin.Daemon
{
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
            var solutionnAnalyzer = new SolutionnAnalyzer();
            solutionnAnalyzer.Analyze(solution);

            CollectUsagesStageProcess usages = process.GetStageProcess<CollectUsagesStageProcess>();
            
            file.ProcessChildren<ITypeDeclaration>(declaration =>
            {
                Registration registration = solutionnAnalyzer.RegisteredTypes.FirstOrDefault(r => r.TypeName == declaration.CLRName);
                if (registration != null)
                {
                    if (usages != null)
                    {
                        usages.SetElementState(declaration.DeclaredElement, UsageState.USED_MASK);
                    }

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
        public string DisplayName { get; set; }

        public ContainerInfo(string castleWindsor, string displayName)
        {
            ContainerName = castleWindsor;
            DisplayName = displayName;
        }
    }
}