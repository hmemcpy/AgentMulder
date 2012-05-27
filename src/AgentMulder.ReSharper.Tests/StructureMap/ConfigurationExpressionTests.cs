using System.Linq;
using AgentMulder.Containers.StructureMap;
using AgentMulder.Containers.StructureMap.Providers;
using AgentMulder.ReSharper.Domain.Containers;
using AgentMulder.ReSharper.Tests.StructureMap.Helpers;
using JetBrains.ReSharper.Psi;
using JetBrains.ReSharper.Psi.CSharp.Tree;
using JetBrains.ReSharper.Psi.Tree;
using NUnit.Framework;

namespace AgentMulder.ReSharper.Tests.StructureMap
{
    [TestStructureMap]
    public class ConfigurationExpressionTests : ComponentRegistrationsTestBase
    {
        protected override string RelativeTestDataPath
        {
            get { return @"StructureMap\ConfigurationExpression"; }
        }

        protected override string RelativeTypesPath
        {
            get { return @"..\..\Types"; }
        }

        protected override IContainerInfo ContainerInfo
        {
            get
            {
                return new StructureMapContainerInfo(new[]
                {
                    new ForPatternsProvider(new UsePatternsProvider())
                });
            }
        }

        [TestCase("HelloStructureMap", "Foo.cs")] // << todo make me pass :)
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