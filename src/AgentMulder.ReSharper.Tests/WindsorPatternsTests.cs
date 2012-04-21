using System.Collections.Generic;
using System.Linq;
using AgentMulder.Containers.CastleWindsor.Patterns;
using AgentMulder.ReSharper.Domain.Registrations;
using AgentMulder.ReSharper.Domain.Search;
using JetBrains.Application.Components;
using JetBrains.ProjectModel;
using JetBrains.ReSharper.Psi.Search;
using JetBrains.ReSharper.Psi.Services.StructuralSearch;
using JetBrains.ReSharper.TestFramework;
using NUnit.Framework;

namespace AgentMulder.ReSharper.Tests
{
    [TestFixture]
    public class WindsorPatternsTests : BaseTestWithSingleProject
    {
        // The source files are located in the solution directory, under Test\Data and the path below, i.e. Test\Data\StructuralSearch\Windsor
        // These files are loaded into the test solution that is being created by this test fixture
        protected override string RelativeTestDataPath
        {
            get { return @"Windsor"; }
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
                    IComponentRegistrationCreator creator = pattern.CreateComponentRegistrationCreator();
                    IEnumerable<IComponentRegistration> registrations = creator.CreateRegistrations(results.ToArray());

                    return registrations.ToList();
                }

                return null;
            }));
        }

        private List<IComponentRegistration> componentRegistrations;
        private List<IComponentRegistrationPattern> patterns;

        public override void SetUp()
        {
            base.SetUp();
            componentRegistrations = new List<IComponentRegistration>();
        }

        [Test]
        public void TestWindsorServiceRegistration()
        {
            patterns = new List<IComponentRegistrationPattern> { new ComponentRegistrationPattern() };

            DoOneTest("WindsorRegistration");

            Assert.That(componentRegistrations.Count, Is.EqualTo(2));
            Assert.That(componentRegistrations.Any((c => c.ToString() == "Implemented by: AgentMulder.TestApplication.Foo")));
            Assert.That(componentRegistrations.Any((c => c.ToString() == "Implemented by: AgentMulder.TestApplication.Bar")));
        }

        [Test]
        public void TestFromTypes()
        {
            patterns = new List<IComponentRegistrationPattern> { new FromTypesPattern() };

            DoOneTest("WindsorRegistration");

            Assert.That(componentRegistrations.Count, Is.EqualTo(1));
            StringAssert.Contains("AgentMulder.TestApplication.Baz", componentRegistrations.First().ToString());
        }
    }
}