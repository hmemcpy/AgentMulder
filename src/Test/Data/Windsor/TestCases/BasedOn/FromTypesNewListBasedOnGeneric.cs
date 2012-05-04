using System;
using System.Collections.Generic;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using TestApplication.Types;

namespace TestApplication.Windsor.TestCases.BasedOn
{
    public class FromTypesNewListBasedOnGeneric : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(
                AllTypes.From(new List<Type> { typeof(Foo), typeof(Bar), typeof(Baz) }).BasedOn<IFoo>(),
                Classes.From(new List<Type> { typeof(Foo), typeof(Bar), typeof(Baz) }).BasedOn<IFoo>(),
                Castle.MicroKernel.Registration.Types.From(new List<Type> { typeof(Foo), typeof(Bar), typeof(Baz) }).BasedOn<IFoo>()
                );
        }
    }
}