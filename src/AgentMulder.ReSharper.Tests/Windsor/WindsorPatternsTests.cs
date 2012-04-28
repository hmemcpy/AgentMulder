using System;
using System.Linq;
using AgentMulder.Containers.CastleWindsor;
using AgentMulder.ReSharper.Domain.Containers;
using AgentMulder.ReSharper.Domain.Registrations;
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

        protected override IContainerInfo ContainerInfo
        {
            get { return new WindsorContainerInfo();}
        }

        [Test]
        public void TestComponentFor()
        {
            DoOneTest("ComponentFor");

            Assert.AreEqual(1, componentRegistrations.Count);
            Assert.That(componentRegistrations.First().ToString(), Is.EqualTo("Implemented by: AgentMulder.ReSharper.Tests.Data.Foo"));
        }

        [Test]
        public void TestComponentForImplementedBy()
        {
            DoOneTest("ComponentForImplementedBy");

            Assert.AreEqual(1, componentRegistrations.Count);
            Assert.That(componentRegistrations.First().ToString(), Is.EqualTo("Implemented by: AgentMulder.ReSharper.Tests.Data.Foo"));
        }

        [Test]
        public void TestComponentForImplementedByWithAdditionalParams()
        {
            DoOneTest("ComponentForImplementedByWithAdditionalParams");

            Assert.AreEqual(1, componentRegistrations.Count);
            Assert.That(componentRegistrations.First().ToString(), Is.EqualTo("Implemented by: AgentMulder.ReSharper.Tests.Data.Bar"));
        }


        [Test]
        public void TestAllTypesFromParams()
        {
            DoOneTest("FromTypesParams");

            Assert.That(componentRegistrations.Count, Is.EqualTo(2));
            Assert.That(componentRegistrations.Any((c => c.ToString() == "Implemented by: AgentMulder.ReSharper.Tests.Data.Baz")));
            Assert.That(componentRegistrations.Any((c => c.ToString() == "Implemented by: AgentMulder.ReSharper.Tests.Data.Bar")));
        }

        [Test]
        public void TestAllTypesFromNewArray()
        {
            DoOneTest("FromTypesNewArray");

            Assert.That(componentRegistrations.Count, Is.EqualTo(2));
            Assert.That(componentRegistrations.Any((c => c.ToString() == "Implemented by: AgentMulder.ReSharper.Tests.Data.Baz")));
            Assert.That(componentRegistrations.Any((c => c.ToString() == "Implemented by: AgentMulder.ReSharper.Tests.Data.Bar")));
        }

        [Test]
        public void TestAllTypesFromNewList()
        {
            DoOneTest("FromTypesNewList");

            Assert.That(componentRegistrations.Count, Is.EqualTo(2));
            Assert.That(componentRegistrations.Any((c => c.ToString() == "Implemented by: AgentMulder.ReSharper.Tests.Data.Baz")));
            Assert.That(componentRegistrations.Any((c => c.ToString() == "Implemented by: AgentMulder.ReSharper.Tests.Data.Bar")));
        }

        [Test]
        public void TestClassesFromParams()
        {
            DoOneTest("FromTypesParams");

            Assert.That(componentRegistrations.Count, Is.EqualTo(2));
            Assert.That(componentRegistrations.Any((c => c.ToString() == "Implemented by: AgentMulder.ReSharper.Tests.Data.Baz")));
            Assert.That(componentRegistrations.Any((c => c.ToString() == "Implemented by: AgentMulder.ReSharper.Tests.Data.Bar")));
        }

        [Test]
        public void TestClassesFromNewArray()
        {
            DoOneTest("FromTypesNewArray");

            Assert.That(componentRegistrations.Count, Is.EqualTo(2));
            Assert.That(componentRegistrations.Any((c => c.ToString() == "Implemented by: AgentMulder.ReSharper.Tests.Data.Baz")));
            Assert.That(componentRegistrations.Any((c => c.ToString() == "Implemented by: AgentMulder.ReSharper.Tests.Data.Bar")));
        }

        [Test]
        public void TestClassesFromNewList()
        {
            DoOneTest("FromTypesNewList");

            Assert.That(componentRegistrations.Count, Is.EqualTo(2));
            Assert.That(componentRegistrations.Any((c => c.ToString() == "Implemented by: AgentMulder.ReSharper.Tests.Data.Baz")));
            Assert.That(componentRegistrations.Any((c => c.ToString() == "Implemented by: AgentMulder.ReSharper.Tests.Data.Bar")));
        }

        [Test]
        public void TestFromThisAssemblyBasedOn()
        {
            DoOneTest("FromThisAssemblyBasedOn");

            Assert.That(componentRegistrations.Count, Is.EqualTo(1));
            IComponentRegistration result = componentRegistrations.First();
            Assert.That(result.ToString(), Is.StringContaining("In module: TestProject"));
            Assert.That(result.ToString(), Is.StringContaining("Based on: AgentMulder.ReSharper.Tests.Data.IFoo"));
            Assert.That(result.ToString(), Is.Not.StringContaining("With Service"));
        }

        [Test]
        public void TestFromThisAssemblyBasedOnWithServiceBase()
        {
            DoOneTest("FromThisAssemblyBasedOnWithServiceBase");

            Assert.That(componentRegistrations.Count, Is.EqualTo(1));
            IComponentRegistration result = componentRegistrations.First();
            Assert.That(result.ToString(), Is.StringContaining("In module: TestProject"));
            Assert.That(result.ToString(), Is.StringContaining("Based on: AgentMulder.ReSharper.Tests.Data.IFoo"));
            Assert.That(result.ToString(), Is.StringContaining("With Service: Base Type"));
        }
    }
}