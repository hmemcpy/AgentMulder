using AgentMulder.Containers.StructureMap;

namespace AgentMulder.ReSharper.Tests.StructureMap
{
    [TestWithNuGetPackage(Packages = new[] { "StructureMap" })]
    public class ScanTests : AgentMulderTestBase<StructureMapContainerInfo>
    {
        protected override string RelativeTestDataPath
        {
            get { return @"StructureMap\ScanTests"; }
        }
    }
}