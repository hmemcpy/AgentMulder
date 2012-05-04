using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;

namespace TestApplication.Windsor.TestCases.BasedOn
{
    public class FromThisAssemblyInNamespaceWithSubnamespaces : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(
                AllTypes.FromThisAssembly().InNamespace("SomeNamespace", true)
                );
        }
    }
}