using AgentMulder.Containers.StructureMap;
using AgentMulder.ReSharper.Domain.Containers;
using AgentMulder.ReSharper.Tests.StructureMap.Helpers;

namespace AgentMulder.ReSharper.Tests.StructureMap
{
    [TestStructureMap]
    public class ContainerTests : AgentMulderTestBase
    {
        protected override string RelativeTestDataPath
        {
            get { return @"StructureMap\ContainerTests"; }
        }

        protected override IContainerInfo ContainerInfo
        {
            get { return new StructureMapContainerInfo(); }
        }
    }
}