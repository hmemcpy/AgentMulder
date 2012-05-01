using System;
using System.Linq;
using AgentMulder.Containers.CastleWindsor;
using AgentMulder.ReSharper.Domain.Containers;
using AgentMulder.ReSharper.Tests.Windsor.Helpers;
using JetBrains.ReSharper.Psi;
using JetBrains.ReSharper.Psi.CSharp.Tree;
using JetBrains.ReSharper.Psi.Tree;
using NUnit.Framework;

namespace AgentMulder.ReSharper.Tests.Windsor
{
    [TestFixture]
    [TestWindsor]
    public class ComponentRegistrationTests : ComponentRegistrationsTestBase
    {
        // The source files are located in the solution directory, under Test\Data and the path below, i.e. Test\Data\StructuralSearch\Windsor
        // These files are loaded into the test solution that is being created by this test fixture
        protected override string RelativeTestDataPath
        {
            get { return @"Windsor\ComponentTestCases"; }
        }

        protected override string RelativeTypesPath
        {
            get { return @"..\..\Types"; }
        }

        protected override IContainerInfo ContainerInfo
        {
            get { return new WindsorContainerInfo(); }
        }

        [TestCase("ComponentFor", "Foo.cs")]
        [TestCase("ComponentForImplementedBy", "Foo.cs")]
        [TestCase("ComponentForImplementedByWithAdditionalParams", "Foo.cs")]
        [TestCase("ComponentForNonGeneric", "Foo.cs")]
        [TestCase("ComponentForImplementedByNonGeneric", "Foo.cs")]
        [TestCase("ComponentForImplementedByNonGenericWithAdditionalParams", "Foo.cs")]
        [TestCase("ComponentForGenericImplementedByNonGeneric", "Foo.cs")]
        [TestCase("ComponentForNonGenericImplementedByGeneric", "Foo.cs")]
        [TestCase("GenericOpenType", "MyList.cs")]
        public void DoTest(string testName, string fileName)
        {
            RunTest(testName, registrations =>
            {
                ICSharpFile file = GetCodeFile(fileName);

                Assert.AreEqual(1, registrations.Count());
                file.ProcessChildren<ITypeDeclaration>(declaration =>
                    Assert.That(registrations.First().IsSatisfiedBy(declaration.DeclaredElement)));
            });
        }
    }
}