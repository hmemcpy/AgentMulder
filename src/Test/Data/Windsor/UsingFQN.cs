// Patterns: 1
// Matches: Foo.cs,Baz.cs 
// NotMatches: Bar.cs 

using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using TestApplication.Types;

namespace TestApplication.Windsor
{
    public class UsingFQN : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(
                AllTypes.FromThisAssembly().BasedOn<IFoo>(),
                Classes.FromThisAssembly().BasedOn<IFoo>(),
                Castle.MicroKernel.Registration.Types.FromThisAssembly().BasedOn<IFoo>()
                );
        }
    }
}