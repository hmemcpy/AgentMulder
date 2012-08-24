using System.Linq;
using AgentMulder.Containers.Catel;
using AgentMulder.ReSharper.Domain.Containers;
using AgentMulder.ReSharper.Tests.Catel.Helpers;
using JetBrains.ReSharper.Psi;
using JetBrains.ReSharper.Psi.CSharp.Tree;
using JetBrains.ReSharper.Psi.Tree;
using NUnit.Framework;

namespace AgentMulder.ReSharper.Tests.Catel
{
    [TestCatel]
    public class ServiceLocatorTests : ComponentRegistrationsTestBase
    {
        protected override string RelativeTestDataPath
        {
            get { return @"Catel"; }
        }

        protected override IContainerInfo ContainerInfo
        {
            get { return new CatelContainerInfo(); }
        }

        [TestCase("RegisterTypeGeneric", new[] { "CommonImpl1.cs" })]
        public void DoTest(string testName, string[] fileNames)
        {
            RunTest(testName, registrations =>
            {
                ICSharpFile[] codeFiles = fileNames.Select(GetCodeFile).ToArray();

                Assert.AreEqual(codeFiles.Length, registrations.Count());
                foreach (var codeFile in codeFiles)
                {
                    codeFile.ProcessChildren<ITypeDeclaration>(declaration =>
                        Assert.That(registrations.Any((r => r.Registration.IsSatisfiedBy(declaration.DeclaredElement)))));
                }
            });
        }
    }
}