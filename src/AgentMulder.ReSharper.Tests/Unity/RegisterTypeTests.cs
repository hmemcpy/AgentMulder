using System.Linq;
using AgentMulder.Containers.Unity;
using AgentMulder.ReSharper.Domain.Containers;
using AgentMulder.ReSharper.Tests.Unity.Helpers;
using JetBrains.ReSharper.Psi;
using JetBrains.ReSharper.Psi.CSharp.Tree;
using JetBrains.ReSharper.Psi.Tree;
using NUnit.Framework;

namespace AgentMulder.ReSharper.Tests.Unity
{
    [TestUnity]
    public class RegisterTypeTests : ComponentRegistrationsTestBase
    {
        protected override string RelativeTestDataPath
        {
            get { return @"Unity"; }
        }

        protected override IContainerInfo ContainerInfo
        {
            get { return new UnityContainerInfo(); }
        }

        [TestCase("RegisterTypeGenericFromTo", "CommonImpl1.cs")]
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