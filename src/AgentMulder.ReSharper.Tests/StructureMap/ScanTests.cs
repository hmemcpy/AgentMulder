using AgentMulder.Containers.StructureMap;
using AgentMulder.ReSharper.Domain.Containers;
using AgentMulder.ReSharper.Tests.StructureMap.Helpers;

namespace AgentMulder.ReSharper.Tests.StructureMap
{
    [TestStructureMap]
    public class ScanTests : AgentMulderTestBase
    {
        protected override string RelativeTestDataPath
        {
            get { return @"StructureMap\ScanTests"; }
        }

        protected override IContainerInfo ContainerInfo
        {
            get { return new StructureMapContainerInfo(); }
        }
    }
}