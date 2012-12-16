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
    public class KernelTests : ComponentRegistrationsTestBase
    {
        protected override string RelativeTestDataPath
        {
            get { return @"Ninject\KernelTestCases"; }
        }

        protected override IContainerInfo ContainerInfo
        {
            get
            {
                return new NinjectContainerInfo(new[]
                {
                    new BindRegistrationProvider(new ToRegistrationProvider())
                });
            }
        }

        [TestCase("GenericOpenType", "MyList.cs")]
        [TestCase("BindGenericToGeneric", "CommonImpl1.cs")]
        [TestCase("BindGenericToNonGeneric", "CommonImpl1.cs")]
        [TestCase("BindNonGenericToGeneric", "CommonImpl1.cs")]
        [TestCase("BindNonGenericToNonGeneric", "CommonImpl1.cs")]
        [TestCase("BindGenericToSelf", "CommonImpl1.cs")]
        [TestCase("BindNonGenericToSelf", "CommonImpl1.cs")]
        [TestCase("RebindGenericToGeneric", "CommonImpl1.cs")]
        [TestCase("RebindGenericToNonGeneric", "CommonImpl1.cs")]
        [TestCase("RebindNonGenericToGeneric", "CommonImpl1.cs")]
        [TestCase("RebindNonGenericToNonGeneric", "CommonImpl1.cs")]
        [TestCase("RebindGenericToSelf", "CommonImpl1.cs")]
        [TestCase("RebindNonGenericToSelf", "CommonImpl1.cs")]
        [TestCase("BindFromKernel", "PageRepository.cs")]
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