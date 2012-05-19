using System.Linq;
using AgentMulder.Containers.Ninject;
using AgentMulder.Containers.Ninject.Providers;
using AgentMulder.ReSharper.Domain.Containers;
using AgentMulder.ReSharper.Tests.Ninject.Helpers;
using JetBrains.ReSharper.Psi;
using JetBrains.ReSharper.Psi.CSharp.Tree;
using JetBrains.ReSharper.Psi.Tree;
using NUnit.Framework;

namespace AgentMulder.ReSharper.Tests.Ninject
{
    [TestNinject]
    public class ModuleTests : ComponentRegistrationsTestBase
    {
        protected override string RelativeTestDataPath
        {
            get { return @"Ninject\ModuleTestCases"; }
        }

        protected override string RelativeTypesPath
        {
            get { return @"..\..\Types"; }
        }

        protected override IContainerInfo ContainerInfo
        {
            get { return new NinjectContainerInfo(new[] { new BindRegistrationProvider(new ToRegistrationProvider()), }); }
        }

        [TestCase("BindGenericToGeneric", "CommonImpl1.cs")]
        [TestCase("BindGenericToNonGeneric", "CommonImpl1.cs")]
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
    }
}