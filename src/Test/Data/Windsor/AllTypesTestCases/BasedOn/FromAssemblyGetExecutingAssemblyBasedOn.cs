using System.Reflection;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using TestApplication.Types;

namespace TestApplication.Windsor.AllTypesTestCases.BasedOn
{
    public class FromAssemblyGetExecutingAssemblyBasedOn : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(
                AllTypes.FromAssembly(Assembly.GetExecutingAssembly()).BasedOn<IFoo>()
                );
        }
 
    }
}