// Patterns: 1
// Matches: InSomeNamespace.cs,InSomeOtherNamespace.cs
// NotMatches: Foo.cs,Bar.cs

using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;

namespace TestApplication.Windsor.TestCases
{
    public class FromThisAssemblyInNamespaceWithSubnamespacesTrue : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(
                AllTypes.FromThisAssembly().InNamespace("SomeNamespace", true),
                Classes.FromThisAssembly().InNamespace("SomeNamespace", true),
                Castle.MicroKernel.Registration.Types.FromThisAssembly().InNamespace("SomeNamespace", true)
                );
        }
    }
}