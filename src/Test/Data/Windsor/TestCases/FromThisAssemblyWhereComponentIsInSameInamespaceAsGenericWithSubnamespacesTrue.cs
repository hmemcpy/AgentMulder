// Patterns: 1
// Matches: InSomeNamespace.cs,InSomeOtherNamespace.cs
// NotMatches: Foo.cs,Bar.cs

using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using SomeNamespace;

namespace TestApplication.Windsor.TestCases
{
    public class FromThisAssemblyWhereComponentIsInSameInamespaceAsGenericWithSubnamespacesTrue : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(
                AllTypes.FromThisAssembly().Where(Component.IsInSameNamespaceAs<IInSomeNamespace>(true)),
                Classes.FromThisAssembly().Where(Component.IsInSameNamespaceAs<IInSomeNamespace>(true)),
                Castle.MicroKernel.Registration.Types.FromThisAssembly().Where(Component.IsInSameNamespaceAs<IInSomeNamespace>(true))
                );
        }
    }
}