using AgentMulder.Containers.StructureMap;

namespace AgentMulder.ReSharper.Tests.StructureMap
{
    [TestWithNuGetPackage(Packages = new[] { "StructureMap:3.1.1.134" })]
    public class ContainerTests : AgentMulderTestBase<StructureMapContainerInfo>
    {
        protected override string RelativeTestDataPath
        {
            get { return @"StructureMap\ContainerTests"; }
        }
    }
}