// Patterns: 1
// Matches: Foo.cs,Baz.cs 
// NotMatches: Bar.cs 

using TestApplication.Types;

namespace TestApplication.Windsor
{
    public class UsingFQN : Castle.MicroKernel.Registration.IWindsorInstaller
    {
        public void Install(Castle.Windsor.IWindsorContainer container, Castle.MicroKernel.SubSystems.Configuration.IConfigurationStore store)
        {
            container.Register(
                Castle.MicroKernel.Registration.AllTypes.FromThisAssembly().BasedOn<IFoo>(),
                Castle.MicroKernel.Registration.Classes.FromThisAssembly().BasedOn<IFoo>(),
                Castle.MicroKernel.Registration.Types.FromThisAssembly().BasedOn<IFoo>()
                );
        }
    }
}