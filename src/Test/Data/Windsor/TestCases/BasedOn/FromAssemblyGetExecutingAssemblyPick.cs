using System.Reflection;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using TestApplication.Types;

namespace TestApplication.Windsor.TestCases.BasedOn
{
    public class FromAssemblyGetExecutingAssemblyPick : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(
                AllTypes.FromAssembly(Assembly.GetExecutingAssembly()).Pick(),
                Classes.FromAssembly(Assembly.GetExecutingAssembly()).Pick(),
                Castle.MicroKernel.Registration.Types.FromAssembly(Assembly.GetExecutingAssembly()).Pick()
                );
        }
    }
}