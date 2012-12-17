// Patterns: 1
// Matches: Foo.cs,Bar.cs
// NotMatches: CommonImpl1.cs

using StructureMap.Configuration.DSL;
using TestApplication.Types;

namespace TestApplication.StructureMap.ScanTests
{
    public class RegistryScanAssemblyContainigTypeNonGeneric : Registry
    {
        public RegistryScanAssemblyContainigTypeNonGeneric()
        {
            Scan(scanner =>
            {
                scanner.AssemblyContainingType(typeof(IFoo));
                scanner.WithDefaultConventions();
            });
        }
    }
}