using AgentMulder.Containers.Ninject;
using AgentMulder.ReSharper.Domain.Containers;

namespace AgentMulder.ReSharper.Tests.Ninject
{
    public class SanityTests : AgentMulderTestBase
    {
        protected override string RelativeTestDataPath
        {
            get { return @"Ninject"; }
        }

        protected override IContainerInfo ContainerInfo
        {
            get { return new NinjectContainerInfo(); }
        }
    }
}