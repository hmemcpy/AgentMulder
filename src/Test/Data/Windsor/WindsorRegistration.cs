using System;
using AgentMulder.ReSharper.Tests.Data;
using Castle.MicroKernel.Registration;
using Castle.Windsor;

namespace TestApplication.Windsor
{
    public class WindsorRegistration
    {
        public WindsorRegistration()
        {
            var container = new WindsorContainer();
            container.Register(
                Component.For<IFoo>().ImplementedBy<Foo>(),

                Component.For<Bar>());
        }
    }
}