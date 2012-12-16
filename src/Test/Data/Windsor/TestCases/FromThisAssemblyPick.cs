using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;

namespace TestApplication.Windsor.TestCases
{
    public class FromThisAssemblyPick : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(
                AllTypes.FromThisAssembly().Pick(),
                Classes.FromThisAssembly().Pick(),
                Castle.MicroKernel.Registration.Types.FromThisAssembly().Pick()
                );
        }
    }
}