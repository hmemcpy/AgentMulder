using System;
using System.Collections.Generic;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using TestApplication.Types;

namespace TestApplication.Windsor.AllTypesTestCases
{
    public class FromTypesNewList : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(
                Castle.MicroKernel.Registration.AllTypes.From(new List<Type> { typeof(Bar), typeof(Baz) }),

                Classes.From(new List<Type> { typeof(Bar), typeof(Baz) })

                );
        }
    }
}