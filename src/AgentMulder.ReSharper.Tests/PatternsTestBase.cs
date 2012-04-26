using System.Collections.Generic;
using AgentMulder.ReSharper.Domain;
using AgentMulder.ReSharper.Domain.Containers;
using AgentMulder.ReSharper.Domain.Registrations;
using AgentMulder.ReSharper.Domain.Search;
using JetBrains.Application.Components;
using JetBrains.ProjectModel;
using JetBrains.ReSharper.Psi.Search;
using JetBrains.ReSharper.TestFramework;

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

        protected override void DoTest(IProject testProject)
        {
            var searchDomainFactory = ShellInstance.GetComponent<SearchDomainFactory>();
            var patternSearcher = new PatternSearcher(testProject.GetSolution(), searchDomainFactory);

            var solutionAnalyzer = new SolutionAnalyzer(patternSearcher, ContainerInfo);
            componentRegistrations.AddRange(solutionAnalyzer.Analyze(Solution));
        }
    }
}