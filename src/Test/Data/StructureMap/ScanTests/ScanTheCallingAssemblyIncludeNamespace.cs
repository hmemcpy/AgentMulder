// Patterns: 1
// Matches: InSomeNamespace.cs,InSomeOtherNamespace.cs
// NotMatches: CommonImpl1.cs,Foo.cs

using StructureMap;
using StructureMap.Graph;

namespace TestApplication.StructureMap.ScanTests
{
    public class ScanTheCallingAssemblyIncludeNamespace
    {
        public ScanTheCallingAssemblyIncludeNamespace()
        {
            var container = new Container(x => x.Scan(scanner =>
            {
                scanner.TheCallingAssembly();
                scanner.IncludeNamespace("SomeNamespace");
                scanner.WithDefaultConventions();
            }));
        } 
    }
}