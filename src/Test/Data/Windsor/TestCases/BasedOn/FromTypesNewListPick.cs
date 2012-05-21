using System;
using System.Collections.Generic;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using TestApplication.Types;

namespace TestApplication.Windsor.TestCases.BasedOn
{
    public class FromTypesNewListPick : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(
                AllTypes.From(new List<Type> { typeof(Foo), typeof(Bar), typeof(Baz) }).Pick(),
                Classes.From(new List<Type> { typeof(Foo), typeof(Bar), typeof(Baz) }).Pick(),
                Castle.MicroKernel.Registration.Types.From(new List<Type> { typeof(Foo), typeof(Bar), typeof(Baz) }).Pick()
                );
        }
    }
}