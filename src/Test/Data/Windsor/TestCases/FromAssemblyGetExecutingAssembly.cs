// Patterns: 0

using System.Reflection;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;

namespace TestApplication.Windsor.TestCases
{
    public class FromAssemblyGetExecutingAssembly : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(
                AllTypes.FromAssembly(Assembly.GetExecutingAssembly()),
                Classes.FromAssembly(Assembly.GetExecutingAssembly()),
                Castle.MicroKernel.Registration.Types.FromAssembly(Assembly.GetExecutingAssembly())
                );
        }
    }
}