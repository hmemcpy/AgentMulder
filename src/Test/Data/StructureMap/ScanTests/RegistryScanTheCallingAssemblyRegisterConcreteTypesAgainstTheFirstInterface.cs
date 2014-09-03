// Patterns: 1
// Matches: Foo.cs,Bar.cs
// NotMatches: PrimitiveArgument.cs

using StructureMap.Configuration.DSL;
using StructureMap.Graph;

namespace TestApplication.StructureMap.ScanTests
{
    public class RegistryScanTheCallingAssemblyRegisterConcreteTypesAgainstTheFirstInterface : Registry
    {
        public RegistryScanTheCallingAssemblyRegisterConcreteTypesAgainstTheFirstInterface()
        {
            Scan(scanner =>
            {
                scanner.TheCallingAssembly();
                scanner.RegisterConcreteTypesAgainstTheFirstInterface();
            });
        }
    }
}