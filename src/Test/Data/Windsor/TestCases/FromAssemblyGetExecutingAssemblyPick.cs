// Patterns: 1
// Matches: Foo.cs,Bar.cs,Baz.cs,FooBar.cs
// NotMatches: IFoo.cs

using System.Reflection;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;

namespace TestApplication.Windsor.TestCases
{
    public class FromAssemblyGetExecutingAssemblyPick : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(
                AllTypes.FromAssembly(Assembly.GetExecutingAssembly()).Pick(),
                Classes.FromAssembly(Assembly.GetExecutingAssembly()).Pick(),
                Castle.MicroKernel.Registration.Types.FromAssembly(Assembly.GetExecutingAssembly()).Pick()
                );
        }
    }
}