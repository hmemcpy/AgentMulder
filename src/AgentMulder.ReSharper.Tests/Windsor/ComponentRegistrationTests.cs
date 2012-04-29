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
    public class ComponentRegistrationTests : PatternsTestBase
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
            Assert.That(componentRegistrations.First().ToString(), Is.EqualTo("Implemented by: AgentMulder.ReSharper.Tests.Data.Foo"));
        }

        [Test]
        public void TestComponentForNonGeneric()
        {
            DoOneTest("ComponentForNonGeneric");

            Assert.AreEqual(1, componentRegistrations.Count);
            IComponentRegistration registration = componentRegistrations.First();
            Assert.That(registration.ToString(), Is.EqualTo("Implemented by: AgentMulder.ReSharper.Tests.Data.Foo"));
        }

        [Test]
        public void TestComponentForImplementedByNonGeneric()
        {
            DoOneTest("ComponentForImplementedByNonGeneric");

            Assert.AreEqual(1, componentRegistrations.Count);
            IComponentRegistration registration = componentRegistrations.First();
            Assert.That(registration.ToString(), Is.EqualTo("Implemented by: AgentMulder.ReSharper.Tests.Data.Foo"));
        }

        [Test]
        public void TestComponentForImplementedByNonGenericWithAdditionalParams()
        {
            DoOneTest("ComponentForImplementedByNonGenericWithAdditionalParams");

            Assert.AreEqual(1, componentRegistrations.Count);
            IComponentRegistration registration = componentRegistrations.First();
            Assert.That(registration.ToString(), Is.EqualTo("Implemented by: AgentMulder.ReSharper.Tests.Data.Foo"));
        }

        [Test]
        public void TestComponentForGenericImplementedByNonGeneric()
        {
            DoOneTest("ComponentForGenericImplementedByNonGeneric");

            Assert.AreEqual(1, componentRegistrations.Count);
            IComponentRegistration registration = componentRegistrations.First();
            Assert.That(registration.ToString(), Is.EqualTo("Implemented by: AgentMulder.ReSharper.Tests.Data.Foo"));
        }


        [Test]
        public void TestComponentForNonGenericImplementedByGeneric()
        {
            DoOneTest("ComponentForNonGenericImplementedByGeneric");

            Assert.AreEqual(1, componentRegistrations.Count);
            IComponentRegistration registration = componentRegistrations.First();
            Assert.That(registration.ToString(), Is.EqualTo("Implemented by: AgentMulder.ReSharper.Tests.Data.Foo"));
        }
    }
}