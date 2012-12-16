// Patterns: 1
// Matches: InSomeNamespace.cs,InSomeOtherNamespace.cs
// NotMatches: Foo.cs,Bar.cs

using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using SomeNamespace;

namespace TestApplication.Windsor.TestCases
{
    public class FromThisAssemblyInSameNamespaceAsNonGenericWithSubnamespacesTrue : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(
                AllTypes.FromThisAssembly().InSameNamespaceAs(typeof(IInSomeNamespace), true),
                Classes.FromThisAssembly().InSameNamespaceAs(typeof(IInSomeNamespace), true),
                Castle.MicroKernel.Registration.Types.FromThisAssembly().InSameNamespaceAs(typeof(IInSomeNamespace), true)
                );
        }
    }
}