using Castle.MicroKernel.Registration;
using Castle.Windsor;
using TestApplication.Data;

namespace TestApplication.Windsor
{
    public class WindsorManual
    {
        public WindsorManual()
        {
            var container = new WindsorContainer();
            container.Register(
                Component.For<IFoo>().ImplementedBy<Foo>());
        } 
    }
}