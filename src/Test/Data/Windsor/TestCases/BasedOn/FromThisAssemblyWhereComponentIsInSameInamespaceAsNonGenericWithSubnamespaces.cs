using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using SomeNamespace;

namespace TestApplication.Windsor.TestCases.BasedOn
{
    public class FromThisAssemblyWhereComponentIsInSameInamespaceAsNonGenericWithSubnamespaces : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(
                AllTypes.FromThisAssembly().Where(Component.IsInSameNamespaceAs(typeof(IInSomeNamespace), true)),
                Classes.FromThisAssembly().Where(Component.IsInSameNamespaceAs(typeof(IInSomeNamespace), true)),
                Castle.MicroKernel.Registration.Types.FromThisAssembly().Where(Component.IsInSameNamespaceAs(typeof(IInSomeNamespace), true))
                );
        }
    }
}