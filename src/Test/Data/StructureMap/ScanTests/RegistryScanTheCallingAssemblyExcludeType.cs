// Patterns: 1
// Matches: Foo.cs
// NotMatches: Bar.cs

using StructureMap.Configuration.DSL;
using TestApplication.Types;

namespace TestApplication.StructureMap.ScanTests
{
    public class RegistryScanTheCallingAssemblyExcludeType : Registry
    {
        public RegistryScanTheCallingAssemblyExcludeType()
        {
            Scan(scanner =>
            {
                scanner.TheCallingAssembly();
                scanner.ExcludeType<Bar>();
                scanner.WithDefaultConventions();
            });
        }
    }
}