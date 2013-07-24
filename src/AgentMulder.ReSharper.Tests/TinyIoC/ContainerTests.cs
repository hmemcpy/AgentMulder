using AgentMulder.Containers.TinyIoC;
using AgentMulder.ReSharper.Domain.Containers;

namespace AgentMulder.ReSharper.Tests.TinyIoC
{
    public class ContainerTests : AgentMulderTestBase
    {
        protected override string RelativeTestDataPath
        {
            get { return @"TinyIoC"; }
        }

        protected override IContainerInfo ContainerInfo
        {
            get { return new TinyIoCContainerInfo(); }
        }
    }
}