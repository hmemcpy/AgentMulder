using System.Collections.Generic;
using System.Linq;
using AgentMulder.Containers.CastleWindsor;
using AgentMulder.ReSharper.Domain.Containers;
using AgentMulder.ReSharper.Domain.Registrations;
using AgentMulder.ReSharper.Domain.Search;
using JetBrains.Application.Components;
using JetBrains.ProjectModel;
using JetBrains.ReSharper.Psi.Search;
using JetBrains.ReSharper.TestFramework;
using NUnit.Framework;

namespace AgentMulder.ReSharper.Tests.PatternMatching
{
    [TestFixture]
    [TestReferences(new[] { "Castle.Windsor.dll" })]
    public class WindsorPatternsTests : PatternSearcherTestsBase
    {
        // The source files are located in the solution directory, under Test\Data and the path below, i.e. Test\Data\StructuralSearch\Windsor
        // These files are loaded into the test solution that is being created by this test fixture
        protected override string RelativeTestDataPath
        {
            get { return @"StructuralSearch\Windsor"; }
        }

        protected override IContainerInfo ContainerInfo
        {
            get { return new WindsorContainerInfo();}
        }

        [Test]
        public void TestWindsorManualRegistration()
        {
            // single registration:
            // Component.For<IFoo>().ImplementedBy<Foo>();

            DoTestSolution("Test01.cs");

            CollectionAssert.IsNotEmpty(ComponentRegistrations);
            IComponentRegistration registration = ComponentRegistrations.First();
            Assert.That(registration, Is.InstanceOf<ConcreteRegistration>());
        }
    }
}