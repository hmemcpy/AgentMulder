using System.Collections.Generic;
using System.Linq;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using TestApplication.Data;

namespace TestApplication.Windsor
{
    public class WindsorInstaller01 : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(
                Component.For<IBar>().ImplementedBy<Bar>(),
                Component.For<IFoo>().ImplementedBy<Foo>());

            container.Register(Component.For<IFoo>());

            container.Register(Component.For(typeof(IFoo)).ImplementedBy(typeof(Foo)));

            container.Register(Component.For(typeof(IFoo)));

            container.Register(Component.For<IEnumerable<object>>().ImplementedBy<List<object>>());
        }
    }

    public class WindsorInstaller02 : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(Classes
                .FromAssemblyInDirectory(new AssemblyFilter(".").FilterByName(an => an.Name.StartsWith("Ploeh.Samples.Booking")))
                .Where(t => !(t.IsGenericType && t.GetGenericTypeDefinition() == typeof(Dispatcher<>)))
                .WithServiceAllInterfaces());
        }
    }

    public class Dispatcher<T>
    {
    }
}