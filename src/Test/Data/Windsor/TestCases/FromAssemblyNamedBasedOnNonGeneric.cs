// Patterns: 1
// Matches: Foo.cs,Baz.cs 
// NotMatches: Bar.cs

using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using TestApplication.Types;

namespace TestApplication.Windsor.TestCases
{
    public class FromAssemblyNamedBasedOnNonGeneric : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            // TestProject is the name of the project that is created in the R# unit test

            container.Register(
                AllTypes.FromAssemblyNamed("TestProject").BasedOn(typeof(IFoo)),
                Classes.FromAssemblyNamed("TestProject").BasedOn(typeof(IFoo)),
                Castle.MicroKernel.Registration.Types.FromAssemblyNamed("TestProject").BasedOn(typeof(IFoo))
                );
        }
    }
}