using AgentMulder.Containers.Catel;
using AgentMulder.ReSharper.Domain.Containers;
using AgentMulder.ReSharper.Tests.Catel.Helpers;

namespace AgentMulder.ReSharper.Tests.Catel
{
    [TestCatel]
    public class ServiceLocatorTests : AgentMulderTestBase
    {
        protected override string RelativeTestDataPath
        {
            get { return @"Catel"; }
        }

        protected override IContainerInfo ContainerInfo
        {
            get { return new CatelContainerInfo(); }
        }
    }
}