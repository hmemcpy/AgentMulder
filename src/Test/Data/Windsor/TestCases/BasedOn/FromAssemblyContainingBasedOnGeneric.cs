using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using TestApplication.Types;

namespace TestApplication.Windsor.TestCases.BasedOn
{
    public class FromAssemblyContainingBasedOnGeneric : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(
                AllTypes.FromAssemblyContaining<IFoo>().BasedOn<IFoo>(),
                Classes.FromAssemblyContaining<IFoo>().BasedOn<IFoo>(),
                Castle.MicroKernel.Registration.Types.FromAssemblyContaining<IFoo>().BasedOn<IFoo>()
                );
        }
    }
}