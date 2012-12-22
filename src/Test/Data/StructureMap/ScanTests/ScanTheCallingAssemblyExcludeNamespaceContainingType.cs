// Patterns: 1
// Matches: InSomeNamespace.cs,InSomeOtherNamespace.cs
// NotMatches: CommonImpl1.cs,Foo.cs

using StructureMap;
using TestApplication.Types;

namespace TestApplication.StructureMap.ScanTests
{
    public class ScanTheCallingAssemblyExcludeNamespaceContainingType
    {
        public ScanTheCallingAssemblyExcludeNamespaceContainingType()
        {
            var container = new Container(x => x.Scan(scanner =>
            {
                scanner.TheCallingAssembly();
                scanner.ExcludeNamespaceContainingType<Foo>();
                scanner.WithDefaultConventions();
            }));
        } 
    }
}