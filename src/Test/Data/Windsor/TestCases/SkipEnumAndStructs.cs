// Patterns: 1
// Matches: Foo.cs,Baz.cs 
// NotMatches: Enum.cs,Struct.cs

using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;

namespace TestApplication.Windsor.TestCases
{
    public class SkipsEnumAndStructs : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(
                AllTypes.FromAssemblyNamed("TestProject").Pick(),
                Classes.FromAssemblyNamed("TestProject").Pick(),
                Castle.MicroKernel.Registration.Types.FromAssemblyNamed("TestProject").Pick()
                );
        }
    }
}