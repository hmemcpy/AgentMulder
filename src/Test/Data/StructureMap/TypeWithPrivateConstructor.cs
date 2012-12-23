// Patterns: 1
// Matches: CommonImpl1.cs
// NotMatches: PrivateCtor.cs

using StructureMap;
using TestApplication.Types;

namespace TestApplication.StructureMap
{
    public class TypeWithPrivateConstructor
    {
        public TypeWithPrivateConstructor()
        {
            var container = new Container(x => x.Scan(scanner =>
            {
                scanner.TheCallingAssembly();
                scanner.AddAllTypesOf<ICommon>();
            }));
        }
    }
}