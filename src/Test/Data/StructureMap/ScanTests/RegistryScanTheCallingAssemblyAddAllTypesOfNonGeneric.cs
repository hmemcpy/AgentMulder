// Patterns: 1
// Matches: CommonImpl1.cs,CommonImpl12.cs
// NotMatches: Foo.cs

using StructureMap.Configuration.DSL;
using TestApplication.Types;

namespace TestApplication.StructureMap.ScanTests
{
    public class RegistryScanTheCallingAssemblyAddAllTypesOfNonGeneric : Registry
    {
        public RegistryScanTheCallingAssemblyAddAllTypesOfNonGeneric()
        {
            Scan(scanner =>
            {
                scanner.TheCallingAssembly();
                scanner.AddAllTypesOf(typeof(ICommon));
            });
        }
    }
}