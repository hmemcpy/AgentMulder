using System.Collections.Generic;
using System.Linq;
using AgentMulder.ReSharper.Domain.Containers;
using AgentMulder.ReSharper.Domain.Registrations;
using AgentMulder.ReSharper.Domain.Search;
using JetBrains.Application.Components;
using JetBrains.ProjectModel;
using JetBrains.ReSharper.Psi.Search;
using JetBrains.ReSharper.TestFramework;

namespace AgentMulder.ReSharper.Tests.PatternMatching
{
    public abstract class PatternSearcherTestsBase : BaseTestWithSingleProject
    {
        private readonly List<IComponentRegistration> componentRegistrations = new List<IComponentRegistration>();

        public IEnumerable<IComponentRegistrationPattern> Patterns
        {
            get { return ContainerInfo.RegistrationPatterns; }
        }

        protected abstract IContainerInfo ContainerInfo { get; }

        public IEnumerable<IComponentRegistration> ComponentRegistrations
        {
            get { return componentRegistrations; }
        }

        protected override void DoTest(IProject testProject)
        {
            var searchDomainFactory = ShellInstance.GetComponent<SearchDomainFactory>();
            var patternSearcher = new PatternSearcher(testProject.GetSolution(), searchDomainFactory);

            componentRegistrations.AddRange(Patterns.SelectMany(pattern => pattern.CreateRegistrations(patternSearcher)));
        }
    }
}