using AgentMulder.Containers.Unity;
using AgentMulder.ReSharper.Domain.Containers;

namespace AgentMulder.ReSharper.Tests.Unity
{
    [TestUnity]
    public class RegisterTypeTests : AgentMulderTestBase
    {
        protected override string RelativeTestDataPath
        {
            get { return @"Unity"; }
        }

        protected override IContainerInfo ContainerInfo
        {
            get { return new UnityContainerInfo(); }
        }
    }
}