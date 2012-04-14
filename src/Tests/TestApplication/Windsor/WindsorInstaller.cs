using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using TestApplication.Data;

namespace TestApplication.Windsor
{
    public class WindsorInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(
                Component.For(typeof(IFoo2)).ImplementedBy(typeof(Foo2)));
        }
    }

    public interface IFoo2
    {
    }

    public class Foo2 : IFoo2
    {
    }
}