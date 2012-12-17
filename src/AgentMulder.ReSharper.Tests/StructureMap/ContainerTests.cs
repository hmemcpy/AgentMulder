using AgentMulder.Containers.StructureMap;
using AgentMulder.Containers.StructureMap.Patterns.For;
using AgentMulder.Containers.StructureMap.Patterns.For.Add;
using AgentMulder.Containers.StructureMap.Patterns.For.Use;
using AgentMulder.ReSharper.Domain.Containers;
using AgentMulder.ReSharper.Domain.Patterns;
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
            get
            {
                var usePatterns = new ComponentImplementationPatternBase[]
                {
                    new UseGeneric(), 
                    new UseNonGeneric(),
                    new AddGeneric(),
                    new AddNonGeneric(), 
                };
                return new StructureMapContainerInfo(new ForGeneric(usePatterns), new ForNonGeneric(usePatterns));
            }
        }
    }
}