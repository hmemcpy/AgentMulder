using AgentMulder.Containers.SimpleInjector;
using AgentMulder.ReSharper.Domain.Containers;
using AgentMulder.ReSharper.Tests.SimpleInjector.Helpers;

namespace AgentMulder.ReSharper.Tests.SimpleInjector
{
    [TestSimpleInjector]
    public class ContainerTests : AgentMulderTestBase
    {
        protected override string RelativeTestDataPath
        {
            get { return @"SimpleInjector"; }
        }

        protected override IContainerInfo ContainerInfo
        {
            get { return new SimpleInjectorContainerInfo(); }
        }
    }
}