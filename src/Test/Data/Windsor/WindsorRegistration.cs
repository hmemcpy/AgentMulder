using AgentMulder.TestApplication;
using Castle.MicroKernel.Registration;
using Castle.Windsor;

namespace TestApplication.Windsor
{
    public class WindsorRegistration
    {
        public readonly WindsorContainer container;

        public WindsorRegistration()
        {
            container = new WindsorContainer();
            container.Register(
                Component.For<IFoo>().ImplementedBy<Foo>(),
                Component.For<Bar>(),
                AllTypes.From(new[] { typeof(Baz) }));
        }
    }
}

namespace AgentMulder.TestApplication
{
    public interface IFoo { }
    public interface IBar { }

    public class Foo : IFoo { }
    public class Bar : IBar { }
    public class Baz : IFoo, IBar { }
}