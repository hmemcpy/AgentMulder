using AgentMulder.Containers.StructureMap;

namespace AgentMulder.ReSharper.Tests.StructureMap
{
    [TestWithNuGetPackage(Packages = new[] { "StructureMap" })]
    public class SanityTests : AgentMulderTestBase<StructureMapContainerInfo>
    {
        protected override string RelativeTestDataPath
        {
            get { return @"StructureMap"; }
        }
    }
}