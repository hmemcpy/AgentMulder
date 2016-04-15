// Patterns: 1
// Matches: InSomeNamespace.cs
// NotMatches: Foo.cs,InSomeOtherNamespace.cs

using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using SomeNamespace;

namespace TestApplication.Windsor.TestCases
{
    public class FromThisAssemblyWhereComponentIsInSameInamespaceAsNonGenericWithSubnamespacesFalse : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(
                AllTypes.FromThisAssembly().Where(Component.IsInSameNamespaceAs(typeof(IInSomeNamespace), false)),
                Classes.FromThisAssembly().Where(Component.IsInSameNamespaceAs(typeof(IInSomeNamespace), false)),
                Castle.MicroKernel.Registration.Types.FromThisAssembly().Where(Component.IsInSameNamespaceAs(typeof(IInSomeNamespace), false))
                );
        }
    }
}