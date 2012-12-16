using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;

namespace TestApplication.Windsor.TestCases
{
    public class FromAssemblyNamedPick : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            // TestProject is the name of the project that is created in the R# unit test

            container.Register(
                AllTypes.FromAssemblyNamed("TestProject").Pick(),
                Classes.FromAssemblyNamed("TestProject").Pick(),
                Castle.MicroKernel.Registration.Types.FromAssemblyNamed("TestProject").Pick()
                );
        }
    }
}