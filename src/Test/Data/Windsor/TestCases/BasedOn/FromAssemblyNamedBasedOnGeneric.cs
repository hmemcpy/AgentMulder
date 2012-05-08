using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using TestApplication.Types;

namespace TestApplication.Windsor.TestCases.BasedOn
{
    public class FromAssemblyNamedBasedOnGeneric : IWindsorInstaller
    {   
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            // TestProject is the name of the project that is created in the R# unit test

            container.Register(
                AllTypes.FromAssemblyNamed("TestProject").BasedOn<IFoo>(),
                Classes.FromAssemblyNamed("TestProject").BasedOn<IFoo>(),
                Castle.MicroKernel.Registration.Types.FromAssemblyNamed("TestProject").BasedOn<IFoo>()
                );
        }
    }
}