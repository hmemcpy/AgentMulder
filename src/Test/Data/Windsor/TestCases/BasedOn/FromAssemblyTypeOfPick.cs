using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using TestApplication.Types;

namespace TestApplication.Windsor.TestCases.BasedOn
{
    public class FromAssemblyTypeOfPick : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(
                AllTypes.FromAssembly(typeof(IFoo).Assembly).Pick(),
                Classes.FromAssembly(typeof(IFoo).Assembly).Pick(),
                Castle.MicroKernel.Registration.Types.FromAssembly(typeof(IFoo).Assembly).Pick()
                );
        }
    }
}