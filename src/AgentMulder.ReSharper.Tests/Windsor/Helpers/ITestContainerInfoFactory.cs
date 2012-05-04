using AgentMulder.ReSharper.Domain.Containers;

namespace AgentMulder.ReSharper.Tests.Windsor.Helpers
{
    public interface ITestContainerInfoFactory
    {
        IContainerInfo GetContainerInfo();
    }
}