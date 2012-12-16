using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using TestApplication.Types;

namespace TestApplication.Windsor.TestCases
{
    public class FromAssemblyTypeOfBasedOn : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(
                AllTypes.FromAssembly(typeof(IFoo).Assembly).BasedOn<IFoo>(),
                Classes.FromAssembly(typeof(IFoo).Assembly).BasedOn<IFoo>(),
                Castle.MicroKernel.Registration.Types.FromAssembly(typeof(IFoo).Assembly).BasedOn<IFoo>()
                );
        }
    }
}