// Patterns: 1
// Matches: CommonImpl12.cs
// NotMatches: Foo.cs

using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using TestApplication.Types;

namespace TestApplication.Windsor.ComponentTestCases
{
    public class ComponentFor2GenericImplementedByGeneric : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(Component.For<ICommon, ICommon2>().ImplementedBy<CommonImpl12>());
        }
    }
}