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
    public class ContainerBuilderTests : ComponentRegistrationsTestBase
    {
        protected override string RelativeTestDataPath
        {
            get { return @"Autofac"; }
        }

        protected override IContainerInfo ContainerInfo
        {
            get { return new AutofacContainerInfo(); }
        }

        [TestCase("RegisterTypeGeneric", new[] { "CommonImpl1.cs" })]
        [TestCase("RegisterTypeNonGeneric", new[] { "CommonImpl1.cs" })]
        [TestCase("RegisterWithLambda", new[] { "Foo.cs" })]
        [TestCase("RegisterWithLambdaTakesDependency", new[] { "TakesDependency.cs" })]
        [TestCase("RegisterWithLambdaInitializer", new[] { "FooBar.cs" })]
        [TestCase("RegisterComplex", new[] { "GoldCard.cs", "StandardCard.cs" })]
        [TestCase("RegisterComplexWithVariable", new[] { "GoldCard.cs" })]
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