// Patterns: 1
// Matches: Foo.cs,Baz.cs 
// NotMatches: Bar.cs 

using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using TestApplication.Types;

namespace TestApplication.Windsor.TestCases
{
    public class FromTypesNewArrayBasedOnGeneric : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(
                AllTypes.From(new[] { typeof(Foo), typeof(Bar), typeof(Baz) }).BasedOn<IFoo>(),
                Classes.From(new[] { typeof(Foo), typeof(Bar), typeof(Baz) }).BasedOn<IFoo>(),
                Castle.MicroKernel.Registration.Types.From(new[] { typeof(Foo), typeof(Bar), typeof(Baz) }).BasedOn<IFoo>()
                );
        }
    }
}