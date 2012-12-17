// Patterns: 1
// Matches: Foo.cs
// NotMatches: Bar.cs 

using StructureMap.Configuration.DSL;
using TestApplication.Types;

namespace TestApplication.StructureMap.ContainerTests
{
    public class RegistryForGenericUseGenericWithAdditionalParams : Registry
    {
        public RegistryForGenericUseGenericWithAdditionalParams()
        {
            For<IFoo>().Singleton().InterceptWith(null).Use<Foo>();
        }
    }
}