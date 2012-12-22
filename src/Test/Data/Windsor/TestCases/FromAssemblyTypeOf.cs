// Patterns: 0

using System;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using TestApplication.Types;

namespace TestApplication.Windsor.TestCases
{
    public class FromAssemblyTypeOf : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(
                AllTypes.FromAssembly(typeof(IFoo).Assembly),
                Classes.FromAssembly(typeof(IFoo).Assembly),
                Castle.MicroKernel.Registration.Types.FromAssembly(typeof(IFoo).Assembly)
                );
        }
    }
}