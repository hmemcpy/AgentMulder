// Patterns: 1
// Matches: InSomeNamespace.cs,InSomeOtherNamespace.cs
// NotMatches: Foo.cs,Bar.cs

using System;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;

namespace TestApplication.Windsor.TestCases
{
    public class FromThisAssemblyWhereComponentIsInInamespaceWithSubnamespacesTrue : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(
                AllTypes.FromThisAssembly().Where(Component.IsInNamespace("SomeNamespace", true)),
                Classes.FromThisAssembly().Where(Component.IsInNamespace("SomeNamespace", true)),
                Castle.MicroKernel.Registration.Types.FromThisAssembly().Where(Component.IsInNamespace("SomeNamespace", true))
                );
        }
    }
}