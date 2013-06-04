// Patterns: 1
// Matches: InSomeNamespace.cs
// NotMatches: Foo.cs,InSomeOtherNamespace.cs

using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;

namespace TestApplication.Windsor.TestCases
{
    public class FromThisAssemblyInNamespaceWithSubnamespacesFalse : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(
                AllTypes.FromThisAssembly().InNamespace("SomeNamespace", false),
                Classes.FromThisAssembly().InNamespace("SomeNamespace", false),
                Castle.MicroKernel.Registration.Types.FromThisAssembly().InNamespace("SomeNamespace", false)
                );
        }
    }
}