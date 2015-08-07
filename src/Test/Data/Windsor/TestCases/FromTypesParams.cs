// Patterns: 0

using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using TestApplication.Types;

namespace TestApplication.Windsor.TestCases
{
    public class FromTypesParams : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(
                AllTypes.From(typeof(Bar), typeof(Baz)),
                Classes.From(typeof(Bar), typeof(Baz)),
                Castle.MicroKernel.Registration.Types.From(typeof(Bar), typeof(Baz))
                );
        }
    }
}