// Patterns: 1
// Matches: InSomeNamespace.cs
// NotMatches: Foo.cs,InSomeOtherNamespace.cs

using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;

namespace TestApplication.Windsor.TestCases
{
    public class FromThisAssemblyWhereComponentIsInInamespaceWithSubnamespacesFalse : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(
                AllTypes.FromThisAssembly().Where(Component.IsInNamespace("SomeNamespace", false)),
                Classes.FromThisAssembly().Where(Component.IsInNamespace("SomeNamespace", false)),
                Castle.MicroKernel.Registration.Types.FromThisAssembly().Where(Component.IsInNamespace("SomeNamespace", false))
                );
        }
    }
}