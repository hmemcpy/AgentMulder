// Patterns: 1
// Matches: Foo.cs,Baz.cs 
// NotMatches: Bar.cs 

namespace TestApplication.Windsor
{
    using Castle.MicroKernel.Registration;
    using Castle.MicroKernel.SubSystems.Configuration;
    using Castle.Windsor;
    using Types;

    public class UsingsInsideNamespace : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(
                AllTypes.FromThisAssembly().BasedOn<IFoo>(),
                Classes.FromThisAssembly().BasedOn<IFoo>(),
                Types.FromThisAssembly().BasedOn<IFoo>()
                );
        }
    }
}