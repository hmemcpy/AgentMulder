// Patterns: 1
// Matches: Single.cs
// NotMatches: CommonImpl1.cs

using StructureMap.Configuration.DSL;

namespace TestApplication.StructureMap.ScanTests
{
    public class RegistryScanTheCallingAssemblySingleImplementationsOfInterface : Registry
    {
        public RegistryScanTheCallingAssemblySingleImplementationsOfInterface()
        {
            Scan(scanner =>
            {
                scanner.TheCallingAssembly();
                scanner.SingleImplementationsOfInterface();
            });
        }
    }
}