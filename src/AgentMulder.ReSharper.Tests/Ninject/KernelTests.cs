using AgentMulder.Containers.Ninject;
using AgentMulder.ReSharper.Domain.Containers;
using AgentMulder.ReSharper.Tests.Ninject.Helpers;

namespace AgentMulder.ReSharper.Tests.Ninject
{
    [TestNinject]
    public class KernelTests : AgentMulderTestBase
    {
        protected override string RelativeTestDataPath
        {
            get { return @"Ninject\KernelTestCases"; }
        }

        protected override IContainerInfo ContainerInfo
        {
            get { return new NinjectContainerInfo(); }
        }
    }
}