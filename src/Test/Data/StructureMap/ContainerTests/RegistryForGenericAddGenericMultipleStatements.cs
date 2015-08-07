// Patterns: 2
// Matches: Foo.cs,Bar.cs
// NotMatches: Baz.cs

using StructureMap.Configuration.DSL;
using TestApplication.Types;

namespace TestApplication.StructureMap.ContainerTests
{
    public class RegistryForGenericAddGenericMultipleStatements : Registry
    {
        public RegistryForGenericAddGenericMultipleStatements()
        {
            For<IFoo>().Add<Foo>();
            For<IBar>().Add<Bar>();
        }
    }
}