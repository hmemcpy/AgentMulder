using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using SomeNamespace;

namespace TestApplication.Windsor.TestCases
{
    public class FromThisAssemblyWhereComponentIsInSameInamespaceAsGenericWithSubnamespaces : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(
                AllTypes.FromThisAssembly().Where(Component.IsInSameNamespaceAs<IInSomeNamespace>(true)),
                Classes.FromThisAssembly().Where(Component.IsInSameNamespaceAs<IInSomeNamespace>(true)),
                Castle.MicroKernel.Registration.Types.FromThisAssembly().Where(Component.IsInSameNamespaceAs<IInSomeNamespace>(true))
                );
        }
    }
}