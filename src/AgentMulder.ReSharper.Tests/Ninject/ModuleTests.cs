using AgentMulder.Containers.Ninject;
using AgentMulder.ReSharper.Domain.Containers;

namespace AgentMulder.ReSharper.Tests.Ninject
{
    [TestNinject]
    public class ModuleTests : AgentMulderTestBase
    {
        protected override string RelativeTestDataPath
        {
            get { return @"Ninject\ModuleTestCases"; }
        }

        protected override IContainerInfo ContainerInfo
        {
            get { return new NinjectContainerInfo(); }
        }
    }
}