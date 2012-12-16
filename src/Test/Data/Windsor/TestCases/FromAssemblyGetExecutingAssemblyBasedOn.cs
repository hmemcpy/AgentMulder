// Patterns: 1
// Matches: Foo.cs,Baz.cs
// NotMatches: Bar.cs

using System.Reflection;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using TestApplication.Types;

namespace TestApplication.Windsor.TestCases
{
    public class FromAssemblyGetExecutingAssemblyBasedOn : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(
                AllTypes.FromAssembly(Assembly.GetExecutingAssembly()).BasedOn<IFoo>(),
                Classes.FromAssembly(Assembly.GetExecutingAssembly()).BasedOn<IFoo>(),
                Castle.MicroKernel.Registration.Types.FromAssembly(Assembly.GetExecutingAssembly()).BasedOn<IFoo>()
                );
        }
    }
}