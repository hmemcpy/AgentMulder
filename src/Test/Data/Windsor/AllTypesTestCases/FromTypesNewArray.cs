using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using TestApplication.Types;

namespace TestApplication.Windsor.AllTypesTestCases
{
    public class FromTypesNewArray : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(
                Castle.MicroKernel.Registration.AllTypes.From(new[] { typeof(Bar), typeof(Baz) }),
                
                Classes.From(new[] { typeof(Bar), typeof(Baz) })
                
                );
        }
    }
}