using AgentMulder.ReSharper.Tests.Data;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;

namespace TestApplication.Windsor
{
    public class FromThisAssemblyBasedOn : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            // this is an incomplete registration - should match 0 types
            container.Register(
                AllTypes.FromThisAssembly().BasedOn<IFoo>()
                );
        }
    }
}