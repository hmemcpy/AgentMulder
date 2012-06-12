using System.Linq;
using AgentMulder.Containers.Autofac;
using AgentMulder.ReSharper.Domain.Containers;
using AgentMulder.ReSharper.Tests.Autofac.Helpers;
using JetBrains.ReSharper.Psi;
using JetBrains.ReSharper.Psi.CSharp.Tree;
using JetBrains.ReSharper.Psi.Tree;
using NUnit.Framework;

namespace AgentMulder.ReSharper.Tests.Autofac
{
    [TestAutofac]
    public class RegisterAssemblyTypesTests : ComponentRegistrationsTestBase
    {
        protected override string RelativeTestDataPath
        {
            get { return @"Autofac\RegisterAssemblyTypesTests"; }
        }

        protected override string RelativeTypesPath
        {
            get { return @"..\..\Types"; }
        }

        protected override IContainerInfo ContainerInfo
        {
            get { return new AutofacContainerInfo(); }
        }

        [TestCase("NoAssembly")]
        public void Sanity(string testName)
        {
            RunTest(testName, registrations => Assert.AreEqual(0, registrations.Count()));
        }

        [TestCase("FromThisAssemblyModuleProperty", new[] { "Foo.cs", "Bar.cs", "Baz.cs", "CommonImpl1.cs" })]
        [TestCase("FromGetExecutingAssembly", new[] { "Foo.cs", "Bar.cs", "Baz.cs", "CommonImpl1.cs" })]
        [TestCase("FromAssemblyTypeOf", new[] { "Foo.cs", "Bar.cs", "Baz.cs", "CommonImpl1.cs" })]
        [TestCase("AllThreeTogether", new[] { "Foo.cs", "Bar.cs", "Baz.cs", "CommonImpl1.cs" })]
        [TestCase("AsGeneric1", new[] { "CommonImpl1.cs" })]
        [TestCase("AsGeneric2", new[] { "CommonImpl12.cs" })]
        [TestCase("AsNonGeneric1", new[] { "CommonImpl1.cs" })]
        [TestCase("AsNonGeneric2", new[] { "CommonImpl12.cs" })]
        [TestCase("AsImplementedInterfaces", new[] { "Foo.cs", "Bar.cs", "Baz.cs", "CommonImpl1.cs" })]
        [TestCase("AssignableToGeneric1", new[] { "StandardCard.cs", "GoldCard.cs" })]
        [TestCase("AssignableToGeneric2", new[] { "CommonImpl12.cs" })]
        [TestCase("AssignableToNonGeneric1", new[] { "StandardCard.cs", "GoldCard.cs" })]
        [TestCase("AssignableToNonGeneric2", new[] { "CommonImpl12.cs" })]
        [TestCase("ExceptGeneric1", new[] { "StandardCard.cs" })]
        public void DoTest(string testName, string[] fileNames)
        {
            RunTest(testName, registrations =>
            {
                ICSharpFile[] codeFiles = fileNames.Select(GetCodeFile).ToArray();

                CollectionAssert.IsNotEmpty(registrations);
                foreach (var codeFile in codeFiles)
                {
                    codeFile.ProcessChildren<ITypeDeclaration>(declaration =>
                        Assert.That(registrations.Any((r => r.Registration.IsSatisfiedBy(declaration.DeclaredElement)))));
                }
            });
        }

        [TestCase("AsGeneric1", new[] { "Foo.cs" })]
        [TestCase("AsGeneric2", new[] { "CommonImpl1.cs" })]
        [TestCase("AsNonGeneric1", new[] { "Foo.cs" })]
        [TestCase("AsNonGeneric2", new[] { "CommonImpl1.cs" })]
        [TestCase("AsImplementedInterfaces", new[] { "GoldCard.cs" })]
        [TestCase("AssignableToGeneric1", new[] { "Foo.cs" })]
        [TestCase("AssignableToGeneric1", new[] { "CommonImpl.cs" })]
        [TestCase("AssignableToNonGeneric1", new[] { "Foo.cs" })]
        [TestCase("AssignableToNonGeneric2", new[] { "CommonImpl.cs" })]
        [TestCase("ExceptGeneric1", new[] { "GoldCard.cs" })]
        [TestCase("ExceptGeneric2", new[] { "GoldCard.cs", "StandardCard.cs" })]
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