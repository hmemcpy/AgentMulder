using AgentMulder.ReSharper.Tests.Data;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;

namespace TestApplication.Windsor
{
    public class ComponentForImplementedByWithAdditionalParams : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(Component.For<IBar>().Named("bar").LifestyleScoped().ImplementedBy<Bar>());
        }
    }
}