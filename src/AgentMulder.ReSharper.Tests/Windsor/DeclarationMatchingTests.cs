using System;
using System.IO;
using System.Linq;
using AgentMulder.Containers.CastleWindsor;
using AgentMulder.ReSharper.Domain;
using AgentMulder.ReSharper.Domain.Containers;
using AgentMulder.ReSharper.Domain.Search;
using AgentMulder.ReSharper.Plugin.Daemon;
using JetBrains.Application.Components;
using JetBrains.Application.Settings;
using JetBrains.Application.src.Settings;
using JetBrains.ProjectModel;
using JetBrains.ReSharper.Daemon;
using JetBrains.ReSharper.Daemon.UsageChecking;
using JetBrains.ReSharper.Psi;
using JetBrains.ReSharper.Psi.Search;
using JetBrains.Util;
using NUnit.Framework;

namespace AgentMulder.ReSharper.Tests.Windsor
{
    public class DeclarationMatchingTests : PatternsTestBase
    {
        protected override IContainerInfo ContainerInfo
        {
            get { return new WindsorContainerInfo();}
        }

        protected override void DoTest()
        {
            var searchDomainFactory = ShellInstance.GetComponent<SearchDomainFactory>();
            var patternSearcher = new PatternSearcher(Solution, searchDomainFactory);

            var solutionAnalyzer = new SolutionAnalyzer(patternSearcher, ContainerInfo);
            componentRegistrations.AddRange(solutionAnalyzer.Analyze(Solution));
        }

        [Test]
        public void UnderTest_Scenario_ExpectedResult()
        {
            WithSingleProject("ComponentFor", (lifetime, project) =>
            {
                IPsiSourceFile sourceFile = GetSourceFile("Foo.cs");

                var process = new TestDaemonProcess(sourceFile);
                var searchDomainFactory = Solution.GetComponent<SearchDomainFactory>();
                var collectUsagesStageProcess = new CollectUsagesStageProcess(process,
                                                                              searchDomainFactory,
                                                                              sourceFile.GetSettingsStore());

                var processStage = new ContainerAnalysisStageProcess(process,
                                                                     collectUsagesStageProcess,
                                                                     searchDomainFactory,
                                                                     @"..\..\..\..\output\Debug\Containers");



                processStage.Execute(result =>
                { 

                });
            });
        }

        public class TestDaemonProcessStage : IDaemonStageProcess
        {
            private readonly IDaemonProcess process;

            public TestDaemonProcessStage(IDaemonProcess process)
            {
                this.process = process;
            }

            public void Execute(Action<DaemonStageResult> commiter)
            {
                
            }

            public IDaemonProcess DaemonProcess
            {
                get { return process; }
            }
        }


        public class TestDaemonProcess : JetBrains.ReSharper.Daemon.Test.TestDaemonProcess
        {
            public TestDaemonProcess(IPsiSourceFile sourceFile)
                : base(sourceFile)
            {
            }

            public override void CommitHighlighters(DaemonCommitContext context)
            {
                
            }
        }



        private IPsiSourceFile GetSourceFile(string fileName)
        {
            IProject project = null;
            PsiManager manager = PsiManager.GetInstance(Solution); ;
            RunGuarded(() =>
            {
                string fullPath = Path.GetFullPath(@"..\..\AgentMulder.ReSharper.Tests.csproj");
                project = Solution.OpenExistingProject(new FileSystemPath(fullPath));
                manager.CommitAllDocuments();
                project.AssertIsValid();
            });

            IProjectFile projectFile = project.GetAllProjectFiles(file => file.Name.Equals(fileName, StringComparison.InvariantCultureIgnoreCase)).FirstOrDefault();
            if (projectFile != null)
            {
                return projectFile.ToSourceFile();
            }

            return null;
        }
    }
}