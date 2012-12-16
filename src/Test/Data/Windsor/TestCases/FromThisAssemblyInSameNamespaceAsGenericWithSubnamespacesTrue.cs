// Patterns: 1
// Matches: InSomeNamespace.cs,InSomeOtherNamespace.cs
// NotMatches: Foo.cs,Bar.cs

using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using SomeNamespace;

namespace TestApplication.Windsor.TestCases
{
    public class FromThisAssemblyInSameNamespaceAsGenericWithSubnamespacesTrue : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(
                AllTypes.FromThisAssembly().InSameNamespaceAs<IInSomeNamespace>(true),
                Classes.FromThisAssembly().InSameNamespaceAs<IInSomeNamespace>(true),
                Castle.MicroKernel.Registration.Types.FromThisAssembly().InSameNamespaceAs<IInSomeNamespace>(true)
                );
        }
    }
}