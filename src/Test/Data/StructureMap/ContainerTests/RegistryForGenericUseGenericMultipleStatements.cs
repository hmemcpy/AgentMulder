// Patterns: 2
// Matches: Foo.cs,Bar.cs
// NotMatches: Baz.cs 

using StructureMap.Configuration.DSL;
using TestApplication.Types;

namespace TestApplication.StructureMap.ContainerTests
{
    public class RegistryForGenericUseGenericMultipleStatements : Registry
    {
        public RegistryForGenericUseGenericMultipleStatements()
        {
            For<IFoo>().Use<Foo>();
            For<IBar>().Use<Bar>();
        }
    }
}