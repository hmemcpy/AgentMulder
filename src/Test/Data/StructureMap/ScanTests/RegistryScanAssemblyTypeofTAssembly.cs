// Patterns: 1
// Matches: Foo.cs,Bar.cs
// NotMatches: CommonImpl1.cs

using StructureMap.Configuration.DSL;
using TestApplication.Types;

namespace TestApplication.StructureMap.ScanTests
{
    public class RegistryScanAssemblyTypeofTAssembly : Registry
    {
        public RegistryScanAssemblyTypeofTAssembly()
        {
            Scan(scanner =>
            {
                scanner.Assembly(typeof(IFoo).Assembly);
                scanner.WithDefaultConventions();
            });
        }
    }
}