using System.Collections.Generic;
using System.Linq;
using AgentMulder.Containers.CastleWindsor;
using AgentMulder.Containers.CastleWindsor.Patterns;
using AgentMulder.Containers.CastleWindsor.Patterns.Component;
using AgentMulder.Containers.CastleWindsor.Patterns.FromTypes;
using AgentMulder.Containers.CastleWindsor.Patterns.FromTypes.BasedOn;
using AgentMulder.ReSharper.Domain.Registrations;
using AgentMulder.ReSharper.Domain.Search;
using JetBrains.Application.Components;
using JetBrains.ProjectModel;
using JetBrains.ReSharper.Psi.Search;
using JetBrains.ReSharper.Psi.Services.StructuralSearch;
using JetBrains.ReSharper.TestFramework;
using NUnit.Framework;

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
                    IEnumerable<IComponentRegistration> registrations = pattern.GetComponentRegistrations(results.ToArray());

                    return registrations.ToList();
                }

                return null;
            }));
        }

        private List<IComponentRegistration> componentRegistrations;
        private List<IRegistrationPattern> patterns;

        public override void SetUp()
        {
            base.SetUp();
            componentRegistrations = new List<IComponentRegistration>();
        }

        [Test]
        public void TestComponentFor()
        {
            patterns = new List<IRegistrationPattern> { new WindsorContainerRegisterPattern(new ComponentForGeneric()) };

            DoOneTest("ComponentFor");

            Assert.AreEqual(1, componentRegistrations.Count);
            Assert.That(componentRegistrations.First().ToString(), Is.EqualTo("Implemented by: AgentMulder.ReSharper.Tests.Data.Foo"));
        }

        [Test]
        public void TestComponentForImplementedBy()
        {
            patterns = new List<IRegistrationPattern> { new WindsorContainerRegisterPattern(new ComponentForGeneric()) };

            DoOneTest("ComponentForImplementedBy");

            Assert.AreEqual(1, componentRegistrations.Count);
            Assert.That(componentRegistrations.First().ToString(), Is.EqualTo("Implemented by: AgentMulder.ReSharper.Tests.Data.Foo"));
        }

        [Test]
        public void TestComponentForImplementedByWithAdditionalParams()
        {
            patterns = new List<IRegistrationPattern> { new WindsorContainerRegisterPattern(new ImplementedByGeneric()) };

            DoOneTest("ComponentForImplementedByWithAdditionalParams");

            Assert.AreEqual(1, componentRegistrations.Count);
            Assert.That(componentRegistrations.First().ToString(), Is.EqualTo("Implemented by: AgentMulder.ReSharper.Tests.Data.Bar"));
        }


        [Test]
        public void TestAllTypesFromParams()
        {
            patterns = new List<IRegistrationPattern> { new WindsorContainerRegisterPattern(new AllTypesFrom()) };

            DoOneTest("FromTypesParams");

            Assert.That(componentRegistrations.Count, Is.EqualTo(2));
            Assert.That(componentRegistrations.Any((c => c.ToString() == "Implemented by: AgentMulder.ReSharper.Tests.Data.Baz")));
            Assert.That(componentRegistrations.Any((c => c.ToString() == "Implemented by: AgentMulder.ReSharper.Tests.Data.Bar")));
        }

        [Test]
        public void TestAllTypesFromNewArray()
        {
            patterns = new List<IRegistrationPattern> { new WindsorContainerRegisterPattern(new AllTypesFrom()) };

            DoOneTest("FromTypesNewArray");

            Assert.That(componentRegistrations.Count, Is.EqualTo(2));
            Assert.That(componentRegistrations.Any((c => c.ToString() == "Implemented by: AgentMulder.ReSharper.Tests.Data.Baz")));
            Assert.That(componentRegistrations.Any((c => c.ToString() == "Implemented by: AgentMulder.ReSharper.Tests.Data.Bar")));
        }

        [Test]
        public void TestAllTypesFromNewList()
        {
            patterns = new List<IRegistrationPattern> { new WindsorContainerRegisterPattern(new AllTypesFrom()) };

            DoOneTest("FromTypesNewList");

            Assert.That(componentRegistrations.Count, Is.EqualTo(2));
            Assert.That(componentRegistrations.Any((c => c.ToString() == "Implemented by: AgentMulder.ReSharper.Tests.Data.Baz")));
            Assert.That(componentRegistrations.Any((c => c.ToString() == "Implemented by: AgentMulder.ReSharper.Tests.Data.Bar")));
        }

        [Test]
        public void TestClassesFromParams()
        {
            patterns = new List<IRegistrationPattern> { new WindsorContainerRegisterPattern(new ClassesFrom()) };

            DoOneTest("FromTypesParams");

            Assert.That(componentRegistrations.Count, Is.EqualTo(2));
            Assert.That(componentRegistrations.Any((c => c.ToString() == "Implemented by: AgentMulder.ReSharper.Tests.Data.Baz")));
            Assert.That(componentRegistrations.Any((c => c.ToString() == "Implemented by: AgentMulder.ReSharper.Tests.Data.Bar")));
        }

        [Test]
        public void TestClassesFromNewArray()
        {
            patterns = new List<IRegistrationPattern> { new WindsorContainerRegisterPattern(new ClassesFrom()) };

            DoOneTest("FromTypesNewArray");

            Assert.That(componentRegistrations.Count, Is.EqualTo(2));
            Assert.That(componentRegistrations.Any((c => c.ToString() == "Implemented by: AgentMulder.ReSharper.Tests.Data.Baz")));
            Assert.That(componentRegistrations.Any((c => c.ToString() == "Implemented by: AgentMulder.ReSharper.Tests.Data.Bar")));
        }

        [Test]
        public void TestClassesFromNewList()
        {
            patterns = new List<IRegistrationPattern> { new WindsorContainerRegisterPattern(new ClassesFrom()) };

            DoOneTest("FromTypesNewList");

            Assert.That(componentRegistrations.Count, Is.EqualTo(2));
            Assert.That(componentRegistrations.Any((c => c.ToString() == "Implemented by: AgentMulder.ReSharper.Tests.Data.Baz")));
            Assert.That(componentRegistrations.Any((c => c.ToString() == "Implemented by: AgentMulder.ReSharper.Tests.Data.Bar")));
        }

        [Test]
        public void TestFromThisAssemblyBasedOn()
        {
            patterns = new List<IRegistrationPattern> { new WindsorContainerRegisterPattern(new AllTypesFromThisAssembly(new BasedOnGeneric())) };

            DoOneTest("FromThisAssemblyBasedOn");

            Assert.That(componentRegistrations.Count, Is.EqualTo(2));
            Assert.That(componentRegistrations.Any((c => c.ToString() == "Implemented by: AgentMulder.ReSharper.Tests.Data.Foo")));
            Assert.That(componentRegistrations.Any((c => c.ToString() == "Implemented by: AgentMulder.ReSharper.Tests.Data.Baz")));
        }
    }
}