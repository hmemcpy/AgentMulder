// Patterns: 1
// Matches: InSomeNamespace.cs
// NotMatches: Foo.cs,InSomeOtherNamespace.cs

using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;

namespace TestApplication.Windsor.TestCases
{
    public class FromThisAssemblyInNamespace : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(
                AllTypes.FromThisAssembly().InNamespace("SomeNamespace"),
                Classes.FromThisAssembly().InNamespace("SomeNamespace"),
                Castle.MicroKernel.Registration.Types.FromThisAssembly().InNamespace("SomeNamespace")
                );
        }
    }
}