using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using TestApplication.Types;

namespace TestApplication.Windsor.ComponentTestCases
{
    public class ComponentForImplementedByWithAdditionalParams : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(Castle.MicroKernel.Registration.Component.For<IFoo>().Named("foo").LifestyleScoped().ImplementedBy<Foo>());
        }
    }
}