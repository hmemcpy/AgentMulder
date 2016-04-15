// Patterns: 0

using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;

namespace TestApplication.Windsor
{
    // THIS FILE IS NOT BEING COMPILED, AND IS SET TO BE IGNORED BY RESHARPER
    // this file tests what happens if the plugin tries to analyze unresolved types
    public class BrokenReference : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(
                Component.For<IDontExist>().ImplementedBy<DontExistImplementation>()
                );
        }
    }
}