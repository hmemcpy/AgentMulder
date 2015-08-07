// Patterns: 0

using System;
using System.Collections.Generic;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using TestApplication.Types;

namespace TestApplication.Windsor.TestCases
{
    public class FromTypesNewList : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(
                AllTypes.From(new List<Type> { typeof(Bar), typeof(Baz) }),
                Classes.From(new List<Type> { typeof(Bar), typeof(Baz) }),
                Castle.MicroKernel.Registration.Types.From(new List<Type> { typeof(Bar), typeof(Baz) })
                );
        }
    }
}