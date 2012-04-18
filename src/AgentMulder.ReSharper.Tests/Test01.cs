using Castle.MicroKernel.Registration;
using Castle.Windsor;
using JetBrains.TestShell.Components.Settings;

namespace AgentMulder.ReSharper.Tests.TestCases
{
    public class Test01
    {
        public Test01()
        {
            var container = new WindsorContainer();
            container.Register(Component.For<IFoo>().ImplementedBy<Foo>());
        } 
    }
}