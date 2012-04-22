using System;
using System.Collections.Generic;
using AgentMulder.TestApplication;
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
                
                Component.For<Bar>(),
                
                AllTypes.From(typeof(Baz), typeof(Bar)),

                Classes.From(typeof(Baz), typeof(Bar)));
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

    public struct MyStruct { }
}