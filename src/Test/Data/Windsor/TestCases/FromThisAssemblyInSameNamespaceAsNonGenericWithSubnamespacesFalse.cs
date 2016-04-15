// Patterns: 1
// Matches: InSomeNamespace.cs
// NotMatches: Foo.cs,InSomeOtherNamespace.cs

using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using SomeNamespace;

namespace TestApplication.Windsor.TestCases
{
    public class FromThisAssemblyInSameNamespaceAsNonGenericWithSubnamespacesFalse : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(
                AllTypes.FromThisAssembly().InSameNamespaceAs(typeof(IInSomeNamespace), false),
                Classes.FromThisAssembly().InSameNamespaceAs(typeof(IInSomeNamespace), false),
                Castle.MicroKernel.Registration.Types.FromThisAssembly().InSameNamespaceAs(typeof(IInSomeNamespace), false)
                );
        }
    }
}