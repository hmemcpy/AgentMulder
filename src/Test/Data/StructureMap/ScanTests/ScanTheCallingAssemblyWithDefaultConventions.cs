// Patterns: 1
// Matches: Foo.cs,Bar.cs
// NotMatches: CommonImpl1.cs

using StructureMap;
using StructureMap.Graph;

namespace TestApplication.StructureMap.ScanTests
{
    public class ScanTheCallingAssemblyWithDefaultConventions
    {
        public ScanTheCallingAssemblyWithDefaultConventions()
        {
            var container = new Container(x => x.Scan(scanner =>
            {
                scanner.TheCallingAssembly();
                scanner.WithDefaultConventions();
            }));
        } 
    }
}