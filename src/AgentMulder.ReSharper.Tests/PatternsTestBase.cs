using System.Collections.Generic;
using AgentMulder.ReSharper.Domain;
using AgentMulder.ReSharper.Domain.Registrations;
using AgentMulder.ReSharper.Domain.Search;
using JetBrains.Application.Components;
using JetBrains.ProjectModel;
using JetBrains.ReSharper.Psi.Search;
using JetBrains.ReSharper.TestFramework;

namespace AgentMulder.ReSharper.Tests
{
    public class PatternsTestBase : BaseTestWithSingleProject
    {
        protected List<IComponentRegistration> componentRegistrations;
        protected List<IRegistrationPattern> patterns;

        public override void SetUp()
        {
            base.SetUp();
            componentRegistrations = new List<IComponentRegistration>();
        }

        protected override void DoTest(IProject testProject)
        {
            var searchDomainFactory = ShellInstance.GetComponent<SearchDomainFactory>();
            var patternSearcher = new PatternSearcher(testProject.GetSolution(), searchDomainFactory);

            var solutionAnalyzer = new SolutionAnalyzer(patternSearcher, new TestContainerInfo(patterns.ToArray()));
            componentRegistrations.AddRange(solutionAnalyzer.Analyze(Solution));
        }
    }
}