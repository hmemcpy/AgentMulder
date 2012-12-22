// Patterns: 1
// Matches: Foo.cs
// NotMatches: Bar.cs 

using StructureMap.Configuration.DSL;
using TestApplication.Types;

namespace TestApplication.StructureMap.ContainerTests
{
    public class RegistryForGenericAddGeneric : Registry
    {
        public RegistryForGenericAddGeneric()
        {
            For<IFoo>().Add<Foo>();
        } 
    }
}