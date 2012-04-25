using System.Collections.Generic;
using System.Linq;
using AgentMulder.ReSharper.Domain.Registrations;
using AgentMulder.ReSharper.Domain.Search;
using JetBrains.Application.Components;
using JetBrains.ProjectModel;
using JetBrains.ReSharper.Psi.Search;
using JetBrains.ReSharper.Psi.Services.StructuralSearch;
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

            componentRegistrations.AddRange(patterns.SelectMany(pattern =>
            {
                IEnumerable<IStructuralMatchResult> results = patternSearcher.Search(pattern);
                if (results != null)
                {
                    IEnumerable<IComponentRegistration> registrations = pattern.GetComponentRegistrations(results.ToArray());

                    return registrations.ToList();
                }

                return null;
            }));
        }
    }
}