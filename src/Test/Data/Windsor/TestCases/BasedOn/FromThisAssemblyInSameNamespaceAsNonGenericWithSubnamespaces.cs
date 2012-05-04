using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using SomeNamespace;

namespace TestApplication.Windsor.TestCases.BasedOn
{
    public class FromThisAssemblyInSameNamespaceAsNonGenericWithSubnamespaces : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(
                AllTypes.FromThisAssembly().InSameNamespaceAs(typeof(IInSomeNamespace), true),
                Classes.FromThisAssembly().InSameNamespaceAs(typeof(IInSomeNamespace), true)
                );
        }
    }
}