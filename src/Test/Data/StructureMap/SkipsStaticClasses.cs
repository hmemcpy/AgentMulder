// Patterns: 1
// Matches: Foo.cs,Baz.cs 
// NotMatches: Static.cs 

using StructureMap;
using TestApplication.Types;

namespace TestApplication.StructureMap
{
    public class SkipsStaticClasses
    {
        public SkipsStaticClasses()
        {
            var container = new Container(x => x.Scan(scanner =>
            {
                scanner.TheCallingAssembly();
                scanner.WithDefaultConventions();
            }));
        }
    }
}