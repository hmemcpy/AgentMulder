// Patterns: 1
// Matches: Foo.cs,Bar.cs
// NotMatches: PrimitiveArgument.cs

using StructureMap;

namespace TestApplication.StructureMap.ScanTests
{
    public class ScanTheCallingAssemblyRegisterConcreteTypesAgainstTheFirstInterface
    {
        public ScanTheCallingAssemblyRegisterConcreteTypesAgainstTheFirstInterface()
        {
            var container = new Container(x => x.Scan(scanner =>
            {
                scanner.TheCallingAssembly();
                scanner.RegisterConcreteTypesAgainstTheFirstInterface();
            }));
        } 
    }
}