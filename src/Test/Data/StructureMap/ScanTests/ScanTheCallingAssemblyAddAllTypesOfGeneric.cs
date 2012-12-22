// Patterns: 1
// Matches: CommonImpl1.cs,CommonImpl12.cs
// NotMatches: Foo.cs

using StructureMap;
using TestApplication.Types;

namespace TestApplication.StructureMap.ScanTests
{
    public class ScanTheCallingAssemblyAddAllTypesOfGeneric
    {
        public ScanTheCallingAssemblyAddAllTypesOfGeneric()
        {
            var container = new Container(x => x.Scan(scanner =>
            {
                scanner.TheCallingAssembly();
                scanner.AddAllTypesOf<ICommon>();
            }));
        } 
    }
}