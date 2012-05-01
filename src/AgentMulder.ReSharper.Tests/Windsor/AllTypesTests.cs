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
    [TestWindsor]
    public class AllTypesTests : ComponentRegistrationsTestBase
    {
        // The source files are located in the solution directory, under Test\Data and the path below, i.e. Test\Data\StructuralSearch\Windsor
        // These files are loaded into the test solution that is being created by this test fixture
        protected override string RelativeTestDataPath
        {
            get { return @"Windsor\AllTypesTestCases"; }
        }

        protected override string RelativeTypesPath
        {
            get { return @"..\..\Types"; }
        }

        protected override IContainerInfo ContainerInfo
        {
            get { return new WindsorContainerInfo(); }
        }

        [TestCase("FromTypesParams")]
        [TestCase("FromTypesNewArray")]
        [TestCase("FromTypesNewList")]
        [TestCase("FromAssemblyTypeOf")]
        [TestCase("FromAssemblyGetExecutingAssembly")]
        public void TestWithEmptyResult(string testName)
        {
            RunTest(testName, registrations => 
                Assert.AreEqual(0, registrations.Count()));
        }

        [TestCase("BasedOn\\FromThisAssemblyBasedOn", new[] { "Foo.cs" })]
        [TestCase("BasedOn\\FromAssemblyTypeOfBasedOn", new[] { "Foo.cs" })]
        [TestCase("BasedOn\\FromAssemblyGetExecutingAssemblyBasedOn", new[] { "Foo.cs" })]
        public void TestWithRegistrations(string testName, params string[] fileNames)
        {
            RunTest(testName, registrations =>
            {
                ICSharpFile[] codeFiles = fileNames.Select(GetCodeFile).ToArray();

                Assert.AreEqual(codeFiles.Length, registrations.Count());
                foreach (var codeFile in codeFiles)
                {
                    codeFile.ProcessChildren<ITypeDeclaration>(declaration =>
                        Assert.That(registrations.Any((registration => registration.IsSatisfiedBy(declaration.DeclaredElement)))));    
                }
            });
        }
    }
}