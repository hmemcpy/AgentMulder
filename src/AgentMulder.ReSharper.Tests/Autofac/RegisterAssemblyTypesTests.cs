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

        [TestCase("FromThisAssemblyModuleProperty", 1, new[] { "Foo.cs", "Bar.cs", "Baz.cs", "CommonImpl1.cs" })]
        [TestCase("FromThisAssemblyGetExecutingAssembly", 1, new[] { "Foo.cs", "Bar.cs", "Baz.cs", "CommonImpl1.cs" })]
        [TestCase("FromThisAssemblyTypeOf", 1, new[] { "Foo.cs", "Bar.cs", "Baz.cs", "CommonImpl1.cs" })]
        [TestCase("AllThreeTogether", 3, new[] { "Foo.cs", "Bar.cs", "Baz.cs", "CommonImpl1.cs" })]
        public void DoTest(string testName, int registrationsCount, string[] fileNames)
        {
            RunTest(testName, registrations =>
            {
                ICSharpFile[] codeFiles = fileNames.Select(GetCodeFile).ToArray();

                Assert.AreEqual(registrationsCount, registrations.Count());
                foreach (var codeFile in codeFiles)
                {
                    codeFile.ProcessChildren<ITypeDeclaration>(declaration =>
                        Assert.That(registrations.Any((r => r.Registration.IsSatisfiedBy(declaration.DeclaredElement)))));
                }
            });
        }
    }
}