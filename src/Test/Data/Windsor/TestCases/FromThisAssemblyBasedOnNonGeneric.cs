using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using TestApplication.Types;

namespace TestApplication.Windsor.TestCases
{
    public class FromThisAssemblyBasedOnNonGeneric : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(
                AllTypes.FromThisAssembly().BasedOn(typeof(IFoo)),
                Classes.FromThisAssembly().BasedOn(typeof(IFoo)),
                Castle.MicroKernel.Registration.Types.FromThisAssembly().BasedOn(typeof(IFoo))
                );
        }
    }
}