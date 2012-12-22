// Patterns: 1
// Matches: Single.cs
// NotMatches: CommonImpl1.cs

using StructureMap;
using StructureMap.Configuration.DSL;

namespace TestApplication.StructureMap.ScanTests
{
    public class ScanTheCallingAssemblySingleImplementationsOfInterface
    {
        public ScanTheCallingAssemblySingleImplementationsOfInterface()
        {
            var container = new Container(x=> x.Scan(scanner =>
            {
                scanner.TheCallingAssembly();
                scanner.SingleImplementationsOfInterface();
            }));
        } 
    }
}