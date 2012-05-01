using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using TestApplication.Types;

namespace TestApplication.Windsor
{
    public class ComponentForImplementedByWithAdditionalParams : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(Component.For<IFoo>().Named("foo").LifestyleScoped().ImplementedBy<Foo>());
        }
    }
}