// Patterns: 1
// Matches: InSomeNamespace.cs
// NotMatches: Foo.cs,InSomeOtherNamespace.cs

using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using SomeNamespace;

namespace TestApplication.Windsor.TestCases
{
    public class FromThisAssemblyWhereComponentIsInSameInamespaceAsNonGeneric : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(
                AllTypes.FromThisAssembly().Where(Component.IsInSameNamespaceAs(typeof(IInSomeNamespace))),
                Classes.FromThisAssembly().Where(Component.IsInSameNamespaceAs(typeof(IInSomeNamespace))),
                Castle.MicroKernel.Registration.Types.FromThisAssembly().Where(Component.IsInSameNamespaceAs(typeof(IInSomeNamespace)))
                );
        }
    }
}