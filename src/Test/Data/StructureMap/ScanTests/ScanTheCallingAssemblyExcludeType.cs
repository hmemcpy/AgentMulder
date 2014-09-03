// Patterns: 1
// Matches: Foo.cs
// NotMatches: Bar.cs

using StructureMap;
using StructureMap.Graph;
using TestApplication.Types;

namespace TestApplication.StructureMap.ScanTests
{
    public class ScanTheCallingAssemblyExcludeType
    {
        public ScanTheCallingAssemblyExcludeType()
        {
            var container = new Container(x => x.Scan(scanner =>
            {
                scanner.TheCallingAssembly();
                scanner.ExcludeType<Bar>();
                scanner.WithDefaultConventions();
            }));
        } 
    }
}