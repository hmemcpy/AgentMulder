using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using AgentMulder.ReSharper.Domain;
using AgentMulder.ReSharper.Domain.Containers;
using AgentMulder.ReSharper.Domain.Registrations;
using AgentMulder.ReSharper.Domain.Search;
using JetBrains.Application.Components;
using JetBrains.ProjectModel;
using JetBrains.ReSharper.Psi;
using JetBrains.ReSharper.Psi.Search;
using JetBrains.ReSharper.Psi.Tree;
using JetBrains.ReSharper.TestFramework;
using JetBrains.Util;
using NUnit.Framework;

namespace AgentMulder.ReSharper.Tests
{
    public abstract class PatternsTestBase : BaseTestWithSingleProject
    {
        protected abstract IContainerInfo ContainerInfo { get; }
        protected List<IComponentRegistration> componentRegistrations;

        public override void SetUp()
        {
            base.SetUp();
            componentRegistrations = new List<IComponentRegistration>();
        }

        protected override void DoTestSolution(params string[] fileSet)
        {
            const string typesRelativeDir = "..\\Types";
            var dataPath = new DirectoryInfo(Path.Combine(SolutionItemsBasePath, typesRelativeDir));
            fileSet = fileSet.Concat(dataPath.GetFiles("*.cs").SelectNotNull(fileInfo => Path.Combine(typesRelativeDir, fileInfo.Name))).ToArray();

            base.DoTestSolution(fileSet);
        }

        protected override void DoTest(IProject testProject)
        {
            var searchDomainFactory = ShellInstance.GetComponent<SearchDomainFactory>();
            var patternSearcher = new PatternSearcher(testProject.GetSolution(), searchDomainFactory);

            var solutionAnalyzer = new SolutionAnalyzer(patternSearcher, ContainerInfo);
            componentRegistrations.AddRange(solutionAnalyzer.Analyze(Solution));
        }
    }
}