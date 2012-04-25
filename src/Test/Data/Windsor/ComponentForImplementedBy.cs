using AgentMulder.ReSharper.Tests.Data;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using Foo = TestApplication.TestData.Foo;

namespace TestApplication.Windsor
{
    public class ComponentForImplementedBy : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(Component.For<IFoo>().ImplementedBy<Foo>());
        }
    }
}