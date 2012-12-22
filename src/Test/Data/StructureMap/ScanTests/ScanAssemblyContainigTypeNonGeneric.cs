// Patterns: 1
// Matches: Foo.cs,Bar.cs
// NotMatches: CommonImpl1.cs

using StructureMap;
using TestApplication.Types;

namespace TestApplication.StructureMap.ScanTests
{
    public class ScanAssemblyContainigTypeNonGeneric
    {
        public ScanAssemblyContainigTypeNonGeneric()
        {
            var container = new Container(x => x.Scan(scanner =>
            {
                scanner.AssemblyContainingType(typeof(IFoo));
                scanner.WithDefaultConventions();
            }));
        }
    }
}