// Patterns: 1
// Matches: HaveAttribute.cs
// NotMatches: Foo.cs

using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using TestApplication.Types;

namespace TestApplication.Windsor.TestCases
{
    public class FromThisAssemblyWhereComponentHasAttributeMethodGroup : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(
                AllTypes.FromThisAssembly().Where(Component.HasAttribute<MyAttribute>),
                Classes.FromThisAssembly().Where(Component.HasAttribute<MyAttribute>),
                Castle.MicroKernel.Registration.Types.FromThisAssembly().Where(Component.HasAttribute<MyAttribute>)
                );
        }
    }
}