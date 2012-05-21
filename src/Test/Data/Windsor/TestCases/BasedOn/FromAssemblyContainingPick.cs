using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using TestApplication.Types;

namespace TestApplication.Windsor.TestCases.BasedOn
{
    public class FromAssemblyContainingPick : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(
                AllTypes.FromAssemblyContaining<IFoo>().Pick(),
                Classes.FromAssemblyContaining<IFoo>().Pick(),
                Castle.MicroKernel.Registration.Types.FromAssemblyContaining<IFoo>().Pick()
                );
        }
    }
}