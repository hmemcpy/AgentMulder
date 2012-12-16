using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;

namespace TestApplication.Windsor.TestCases
{
    public class FromThisAssemblyWhereComponentIsInInamespace : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(
                AllTypes.FromThisAssembly().Where(Component.IsInNamespace("SomeNamespace")),
                Classes.FromThisAssembly().Where(Component.IsInNamespace("SomeNamespace")),
                Castle.MicroKernel.Registration.Types.FromThisAssembly().Where(Component.IsInNamespace("SomeNamespace"))
                );
        }
    }
}