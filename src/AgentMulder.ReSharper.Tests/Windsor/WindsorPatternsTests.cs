using System.Collections.Generic;
using System.Linq;
using AgentMulder.Containers.CastleWindsor.Patterns.FromTypes;
using AgentMulder.ReSharper.Domain.Registrations;
using AgentMulder.ReSharper.Domain.Search;
using JetBrains.Application.Components;
using JetBrains.ProjectModel;
using JetBrains.ReSharper.Psi.Search;
using JetBrains.ReSharper.Psi.Services.StructuralSearch;
using JetBrains.ReSharper.TestFramework;
using NUnit.Framework;
using ComponentRegistration = AgentMulder.Containers.CastleWindsor.Patterns.Component.ComponentRegistration;

namespace AgentMulder.ReSharper.Tests.Windsor
{
    [TestFixture]
    [TestWindsor]
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
        private List<IRegistration> patterns;

        public override void SetUp()
        {
            base.SetUp();
            componentRegistrations = new List<IComponentRegistration>();
        }

        [Test]
        public void TestWindsorServiceRegistration()
        {
            patterns = new List<IRegistration> { new ComponentRegistration() };

            DoOneTest("WindsorRegistration");

            Assert.That(componentRegistrations.Count, Is.EqualTo(2));
            Assert.That(componentRegistrations.Any((c => c.ToString() == "Implemented by: AgentMulder.ReSharper.Tests.Data.Foo")));
            Assert.That(componentRegistrations.Any((c => c.ToString() == "Implemented by: AgentMulder.ReSharper.Tests.Data.Bar")));
        }

        [Test]
        public void TestAllTypesFromParams()
        {
            patterns = new List<IRegistration> { new AllTypesFrom() };

            DoOneTest("FromTypesParams");

            Assert.That(componentRegistrations.Count, Is.EqualTo(2));
            Assert.That(componentRegistrations.Any((c => c.ToString() == "Implemented by: AgentMulder.ReSharper.Tests.Data.Baz")));
            Assert.That(componentRegistrations.Any((c => c.ToString() == "Implemented by: AgentMulder.ReSharper.Tests.Data.Bar")));
        }

        [Test]
        public void TestAllTypesFromNewArray()
        {
            patterns = new List<IRegistration> { new AllTypesFrom() };

            DoOneTest("FromTypesNewArray");

            Assert.That(componentRegistrations.Count, Is.EqualTo(2));
            Assert.That(componentRegistrations.Any((c => c.ToString() == "Implemented by: AgentMulder.ReSharper.Tests.Data.Baz")));
            Assert.That(componentRegistrations.Any((c => c.ToString() == "Implemented by: AgentMulder.ReSharper.Tests.Data.Bar")));
        }

        [Test]
        public void TestAllTypesFromNewList()
        {
            patterns = new List<IRegistration> { new AllTypesFrom() };

            DoOneTest("FromTypesNewList");

            Assert.That(componentRegistrations.Count, Is.EqualTo(2));
            Assert.That(componentRegistrations.Any((c => c.ToString() == "Implemented by: AgentMulder.ReSharper.Tests.Data.Baz")));
            Assert.That(componentRegistrations.Any((c => c.ToString() == "Implemented by: AgentMulder.ReSharper.Tests.Data.Bar")));
        }

        [Test]
        public void TestClassesFromParams()
        {
            patterns = new List<IRegistration> { new ClassesFrom() };

            DoOneTest("FromTypesParams");

            Assert.That(componentRegistrations.Count, Is.EqualTo(2));
            Assert.That(componentRegistrations.Any((c => c.ToString() == "Implemented by: AgentMulder.ReSharper.Tests.Data.Baz")));
            Assert.That(componentRegistrations.Any((c => c.ToString() == "Implemented by: AgentMulder.ReSharper.Tests.Data.Bar")));
        }

        [Test]
        public void TestClassesFromNewArray()
        {
            patterns = new List<IRegistration> { new ClassesFrom() };

            DoOneTest("FromTypesNewArray");

            Assert.That(componentRegistrations.Count, Is.EqualTo(2));
            Assert.That(componentRegistrations.Any((c => c.ToString() == "Implemented by: AgentMulder.ReSharper.Tests.Data.Baz")));
            Assert.That(componentRegistrations.Any((c => c.ToString() == "Implemented by: AgentMulder.ReSharper.Tests.Data.Bar")));
        }

        [Test]
        public void TestClassesFromNewList()
        {
            patterns = new List<IRegistration> { new ClassesFrom() };

            DoOneTest("FromTypesNewList");

            Assert.That(componentRegistrations.Count, Is.EqualTo(2));
            Assert.That(componentRegistrations.Any((c => c.ToString() == "Implemented by: AgentMulder.ReSharper.Tests.Data.Baz")));
            Assert.That(componentRegistrations.Any((c => c.ToString() == "Implemented by: AgentMulder.ReSharper.Tests.Data.Bar")));
        }

        [Test, Ignore("Does not work yet")]
        public void TestFromAssembly()
        {
            patterns = new List<IRegistration> { new AllTypesFromAssembly() };

            DoOneTest("WindsorRegistration");

            Assert.That(componentRegistrations.Count, Is.EqualTo(3));
            Assert.That(componentRegistrations.Any((c => c.ToString() == "Implemented by: AgentMulder.ReSharper.Tests.Data.Foo")));
            Assert.That(componentRegistrations.Any((c => c.ToString() == "Implemented by: AgentMulder.ReSharper.Tests.Data.Bar")));
            Assert.That(componentRegistrations.Any((c => c.ToString() == "Implemented by: AgentMulder.ReSharper.Tests.Data.Baz")));
        }
    }
}