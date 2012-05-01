using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using TestApplication.Types;

namespace TestApplication.Windsor.ComponentTestCases
{
    public class ComponentForImplementedByNonGenericWithAdditionalParams : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(Castle.MicroKernel.Registration.Component.For(typeof(IFoo)).Named("foo").LifestyleScoped().ImplementedBy(typeof(Foo)).Forward<IBar>());
        }
    }
}