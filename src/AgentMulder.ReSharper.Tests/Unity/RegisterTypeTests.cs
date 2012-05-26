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

        [TestCase("RegisterTypeGenericFromTo", new[]{ "CommonImpl1.cs" })]
        [TestCase("RegisterTypeGenericFromToChained", new[] { "CommonImpl1.cs", "CommonImpl12.cs" })]
        [TestCase("RegisterTypeGenericFromTo3TimesIsTheCharm", new[] { "CommonImpl1.cs", "CommonImpl12.cs", "Foo.cs" })]
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