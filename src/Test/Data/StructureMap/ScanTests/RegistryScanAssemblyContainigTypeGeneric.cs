// Patterns: 1
// Matches: Foo.cs,Bar.cs
// NotMatches: CommonImpl1.cs

using StructureMap.Configuration.DSL;
using TestApplication.Types;

namespace TestApplication.StructureMap.ScanTests
{
    public class RegistryScanAssemblyContainigTypeGeneric : Registry
    {
        public RegistryScanAssemblyContainigTypeGeneric()
        {
            Scan(scanner =>
            {
                scanner.AssemblyContainingType<IFoo>();
                scanner.WithDefaultConventions();
            });
        }
    }
}