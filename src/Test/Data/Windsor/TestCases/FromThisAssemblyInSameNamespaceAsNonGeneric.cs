using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using SomeNamespace;

namespace TestApplication.Windsor.TestCases
{
    public class FromThisAssemblyInSameNamespaceAsNonGeneric : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(
                AllTypes.FromThisAssembly().InSameNamespaceAs(typeof(IInSomeNamespace)),
                Classes.FromThisAssembly().InSameNamespaceAs(typeof(IInSomeNamespace)),
                Castle.MicroKernel.Registration.Types.FromThisAssembly().InSameNamespaceAs(typeof(IInSomeNamespace))
                );
        }
    }
}