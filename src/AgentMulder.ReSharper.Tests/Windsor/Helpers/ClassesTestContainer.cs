using AgentMulder.Containers.CastleWindsor;
using AgentMulder.Containers.CastleWindsor.Providers;
using AgentMulder.ReSharper.Domain.Containers;

namespace AgentMulder.ReSharper.Tests.Windsor.Helpers
{
    class ClassesTestContainer : ITestContainerInfoFactory
    {
        public IContainerInfo GetContainerInfo()
        {
            return new WindsorContainerInfo(new[] { new ClassesRegistrationProvider(new BasedOnRegistrationProvider(new WithServicesRegistrationProvider())) });
        }
    }
}