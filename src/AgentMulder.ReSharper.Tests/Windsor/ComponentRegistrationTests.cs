using System;
using System.Linq;
using AgentMulder.Containers.CastleWindsor;
using AgentMulder.Containers.CastleWindsor.Providers;
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
            get
            {
                return new WindsorContainerInfo(new[]
                {
                    new ComponentRegistrationProvider(new ImplementedByRegistrationProvider())
                });
            }
        }

        [TestCase("GenericOpenType", "MyList.cs")]
        [TestCase("ComponentFor", "Foo.cs")]
        [TestCase("ComponentForImplementedBy", "Foo.cs")]
        [TestCase("ComponentForImplementedByWithAdditionalParams", "Foo.cs")]
        [TestCase("ComponentForNonGeneric", "Foo.cs")]
        [TestCase("ComponentForImplementedByNonGeneric", "Foo.cs")]
        [TestCase("ComponentForImplementedByNonGenericWithAdditionalParams", "Foo.cs")]
        [TestCase("ComponentForGenericImplementedByNonGeneric", "Foo.cs")]
        [TestCase("ComponentForNonGenericImplementedByGeneric", "Foo.cs")]
        [TestCase("ComponentFor2GenericImplementedByGeneric", "CommonImpl12.cs")]
        [TestCase("ComponentFor3GenericImplementedByGeneric", "CommonImpl123.cs")]
        [TestCase("ComponentFor4GenericImplementedByGeneric", "CommonImpl1234.cs")]
        [TestCase("ComponentFor5GenericImplementedByGeneric", "CommonImpl1234.cs")]
        public void DoTest(string testName, string fileName)
        {
            RunTest(testName, registrations =>
            {
                ICSharpFile file = GetCodeFile(fileName);

                Assert.AreEqual(1, registrations.Count());
                file.ProcessChildren<ITypeDeclaration>(declaration =>
                    Assert.That(registrations.First().Registration.IsSatisfiedBy(declaration.DeclaredElement)));
            });
        }

        [TestCase("ComponentForImplementedBy", new[] { "IFoo.cs" })]
        public void ExcludeTest(string testName, string[] fileNamesToExclude)
        {
            RunTest(testName, registrations =>
            {
                ICSharpFile[] codeFiles = fileNamesToExclude.Select(GetCodeFile).ToArray();

                CollectionAssert.IsNotEmpty(registrations);
                foreach (var codeFile in codeFiles)
                {
                    codeFile.ProcessChildren<ITypeDeclaration>(declaration =>
                        Assert.That(registrations.All((r => !r.Registration.IsSatisfiedBy(declaration.DeclaredElement)))));
                }
            });
        }
    }
}