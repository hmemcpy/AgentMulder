using Castle.MicroKernel.Registration;
using Castle.Windsor;

namespace AgentMulder.TestCases
{
    // todo not supported
    public class Service
    {
        public readonly WindsorContainer container;

        public Service()
        {
            container = new WindsorContainer();
            container.Register(Component.For<Foo>());
        }

        public interface IFoo { }

        public class Foo : IFoo { }
    }
}