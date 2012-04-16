using System;
using System.IO;
using System.Linq;
using System.Reflection;
using AgentMulder.Core;
using AgentMulder.Core.TypeSystem;
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
        private readonly CollectUsagesStageProcess usagesStageProcess;

        public ContainerAnalysisStageProcess(IDaemonProcess process, CollectUsagesStageProcess usagesStageProcess)
        {
            this.process = process;
            this.usagesStageProcess = usagesStageProcess;
        }

        public void Execute(Action<DaemonStageResult> commiter)
        {
            PsiManager manager = PsiManager.GetInstance(process.Solution);
            var file = manager.GetPsiFile(process.SourceFile, CSharpLanguage.Instance) as ICSharpFile;
            if (file == null)
            {
                return;
            }

            ISolution solution = SolutonReader.ReadSolution(process.Solution.SolutionFilePath.FullPath);
            var solutionnAnalyzer = new SolutionnAnalyzer(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location));
            solutionnAnalyzer.Analyze(solution);
            
            file.ProcessChildren<ITypeDeclaration>(declaration =>
            {
                Registration registration = solutionnAnalyzer.RegisteredTypes.FirstOrDefault(r => r.TypeName == declaration.CLRName);
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