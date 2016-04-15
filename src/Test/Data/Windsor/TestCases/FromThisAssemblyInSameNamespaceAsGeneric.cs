// Patterns: 1
// Matches: InSomeNamespace.cs
// NotMatches: Foo.cs,InSomeOtherNamespace.cs

using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using SomeNamespace;

namespace TestApplication.Windsor.TestCases
{
    public class FromThisAssemblyInSameNamespaceAsGeneric : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(
                AllTypes.FromThisAssembly().InSameNamespaceAs<IInSomeNamespace>(),
                Classes.FromThisAssembly().InSameNamespaceAs<IInSomeNamespace>(),
                Castle.MicroKernel.Registration.Types.FromThisAssembly().InSameNamespaceAs<IInSomeNamespace>()
                );
        }
    }
}