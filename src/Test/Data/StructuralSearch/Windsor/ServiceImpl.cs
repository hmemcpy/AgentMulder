using Castle.MicroKernel.Registration;
using Castle.Windsor;

namespace AgentMulder.TestCases
{
    public class ManualRegistration
    {
        public ManualRegistration()
        {
            var container = new WindsorContainer();
            container.Register(Component.For<IFoo>().ImplementedBy<Foo>());
        }

        public interface IFoo { }

        public class Foo : IFoo { }
    }
}