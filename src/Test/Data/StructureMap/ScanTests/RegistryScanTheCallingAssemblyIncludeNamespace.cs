// Patterns: 1
// Matches: InSomeNamespace.cs,InSomeOtherNamespace.cs
// NotMatches: CommonImpl1.cs,Foo.cs

using StructureMap.Configuration.DSL;
using StructureMap.Graph;

namespace TestApplication.StructureMap.ScanTests
{
    public class RegistryScanTheCallingAssemblyIncludeNamespace : Registry
    {
        public RegistryScanTheCallingAssemblyIncludeNamespace()
        {
            Scan(scanner =>
            {
                scanner.TheCallingAssembly();
                scanner.IncludeNamespace("SomeNamespace");
                scanner.WithDefaultConventions();
            });
        }
    }
}