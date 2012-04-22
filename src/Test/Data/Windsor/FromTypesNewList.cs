using System;
using System.Collections.Generic;
using AgentMulder.ReSharper.Tests.Data;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;

namespace TestApplication.Windsor
{
    public class FromTypesNewList : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(
                AllTypes.From(new List<Type> { typeof(Bar), typeof(Baz) }),

                Classes.From(new List<Type> { typeof(Bar), typeof(Baz) })

                );
        }
    }
}