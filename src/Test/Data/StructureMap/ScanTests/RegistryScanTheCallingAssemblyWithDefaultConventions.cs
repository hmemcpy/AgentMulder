// Patterns: 1
// Matches: Foo.cs,Bar.cs
// NotMatches: CommonImpl1.cs

using StructureMap.Configuration.DSL;
using StructureMap.Graph;

namespace TestApplication.StructureMap.ScanTests
{
    public class RegistryScanTheCallingAssemblyWithDefaultConventions : Registry
    {
        public RegistryScanTheCallingAssemblyWithDefaultConventions()
        {
            Scan(scanner =>
            {
                scanner.TheCallingAssembly();
                scanner.WithDefaultConventions();
            });
        }
    }
}