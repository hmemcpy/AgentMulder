// Patterns: 1
// Matches: Foo.cs
// NotMatches: Bar.cs

using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using TestApplication.Types;

namespace TestApplication.Windsor.ComponentTestCases
{
    public class ComponentForImplementedByNonGeneric : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(Castle.MicroKernel.Registration.Component.For(typeof(IFoo)).ImplementedBy(typeof(Foo)));
        }
    }
}