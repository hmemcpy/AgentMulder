using System.Collections.Generic;
using System.Linq;
using AgentMulder.Containers.CastleWindsor.Patterns;
using AgentMulder.Containers.CastleWindsor.Patterns.Component;
using AgentMulder.Containers.CastleWindsor.Patterns.FromTypes;
using AgentMulder.Containers.CastleWindsor.Patterns.FromTypes.BasedOn;
using AgentMulder.Containers.CastleWindsor.Patterns.FromTypes.WithService;
using AgentMulder.ReSharper.Domain.Registrations;
using AgentMulder.ReSharper.Domain.Search;
using NUnit.Framework;

namespace AgentMulder.ReSharper.Tests.Windsor
{
    [TestFixture]
    [TestWindsor]
    public class WindsorPatternsTests : PatternsTestBase
    {
        // The source files are located in the solution directory, under Test\Data and the path below, i.e. Test\Data\StructuralSearch\Windsor
        // These files are loaded into the test solution that is being created by this test fixture
        protected override string RelativeTestDataPath
        {
            get { return @"Windsor"; }
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
            patterns = new List<IRegistrationPattern> { new WindsorContainerRegisterPattern(new ComponentForGeneric(new ImplementedByGeneric())) };

            DoOneTest("ComponentForImplementedBy");

            Assert.AreEqual(1, componentRegistrations.Count);
            Assert.That(componentRegistrations.First().ToString(), Is.EqualTo("Implemented by: AgentMulder.ReSharper.Tests.Data.Foo"));
        }

        [Test]
        public void TestComponentForImplementedByWithAdditionalParams()
        {
            patterns = new List<IRegistrationPattern> { new WindsorContainerRegisterPattern(new ComponentForGeneric(new ImplementedByGeneric())) };

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
            patterns = new List<IRegistrationPattern> 
            { 
                new WindsorContainerRegisterPattern(
                    new AllTypesFromThisAssembly(
                        new BasedOnGeneric(
                            new WithServiceBase()))) 
            };

            DoOneTest("FromThisAssemblyBasedOn");

            Assert.That(componentRegistrations.Count, Is.EqualTo(0));
        }

        [Test]
        public void TestFromThisAssemblyBasedOnWithServiceBase()
        {
            patterns = new List<IRegistrationPattern> 
            { 
                new WindsorContainerRegisterPattern(
                    new AllTypesFromThisAssembly(
                        new BasedOnGeneric(
                            new WithServiceBase()))) 
            };

            DoOneTest("FromThisAssemblyBasedOnWithServiceBase");

            Assert.That(componentRegistrations.Count, Is.EqualTo(1));
            IComponentRegistration result = componentRegistrations.First();
            Assert.That(result.ToString(), Is.StringContaining("In module: TestProject"));
            Assert.That(result.ToString(), Is.StringContaining("Based on: AgentMulder.ReSharper.Tests.Data.IFoo"));
            Assert.That(result.ToString(), Is.StringContaining("With Service: Base Type"));
        }
    }
}