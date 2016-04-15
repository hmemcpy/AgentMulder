// Patterns: 1
// Matches: Foo.cs,Bar.cs,Baz.cs
// NotMatches: IFoo.cs

using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using TestApplication.Types;

namespace TestApplication.Windsor.TestCases
{
    public class FromTypesParamsPick : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(
                AllTypes.From(typeof(Foo), typeof(Bar), typeof(Baz)).Pick(),
                Classes.From(typeof(Foo), typeof(Bar), typeof(Baz)).Pick(),
                Castle.MicroKernel.Registration.Types.From(typeof(Foo), typeof(Bar), typeof(Baz)).Pick()
                );
        }
    }
}