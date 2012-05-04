using AgentMulder.Containers.CastleWindsor;
using AgentMulder.ReSharper.Domain.Containers;

namespace AgentMulder.ReSharper.Tests.Windsor.Helpers
{
    class EmptyTestContainer : ITestContainerInfoFactory
    {
        public IContainerInfo GetContainerInfo()
        {
            return new WindsorContainerInfo();
        }
    }
}