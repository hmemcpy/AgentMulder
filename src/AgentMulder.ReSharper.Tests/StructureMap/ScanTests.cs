using System.Linq;
using AgentMulder.Containers.StructureMap;
using AgentMulder.ReSharper.Domain.Containers;
using AgentMulder.ReSharper.Tests.StructureMap.Helpers;
using JetBrains.ReSharper.Psi;
using JetBrains.ReSharper.Psi.CSharp.Tree;
using JetBrains.ReSharper.Psi.Tree;
using NUnit.Framework;

namespace AgentMulder.ReSharper.Tests.StructureMap
{
    [TestStructureMap]
    public class ScanTests : ComponentRegistrationsTestBase
    {
        protected override string RelativeTestDataPath
        {
            get { return @"StructureMap\ScanTests"; }
        }

        protected override string RelativeTypesPath
        {
            get { return @"..\..\Types"; }
        }

        protected override IContainerInfo ContainerInfo
        {
            get { return new StructureMapContainerInfo(); }
        }

        [TestCase("ScanNoAssemblyStatement", 0, new string[0])]
        [TestCase("ScanTheCallingAssembly", 0, new string[0])]
        [TestCase("ScanTheCallingAssemblyWithDefaultConventions", 1, new[] { "Foo.cs", "Bar.cs" })]
        [TestCase("ScanTheCallingAssemblyAddAllTypesOfGeneric", 1, new[] { "CommonImpl1.cs", "CommonImpl12.cs" })]
        [TestCase("ScanTheCallingAssemblyAddAllTypesOfNonGeneric", 1, new[] { "CommonImpl1.cs", "CommonImpl12.cs" })]
        public void DoTest(string testName, int registrationsCount, string[] fileNames)
        {
            RunTest(testName, registrations =>
            {
                ICSharpFile[] codeFiles = fileNames.Select(GetCodeFile).ToArray();

                Assert.AreEqual(registrationsCount, registrations.Count);
                foreach (var codeFile in codeFiles)
                {
                    codeFile.ProcessChildren<ITypeDeclaration>(declaration =>
                        Assert.That(registrations.Any((r => r.Registration.IsSatisfiedBy(declaration.DeclaredElement)))));
                }
            });
        }

        [TestCase("ScanTheCallingAssemblyWithDefaultConventions", new[] { "CommonImpl1.cs" })]
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

        [TestCase("ScanTheCallingAssembly")]
        public void EmptyTest(string testName)
        {
            RunTest(testName, CollectionAssert.IsEmpty);
        }
    }
}