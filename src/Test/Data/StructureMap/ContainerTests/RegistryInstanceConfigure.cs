// Patterns: 1
// Matches: Foo.cs
// NotMatches: Bar.cs 

using TestApplication.Types;

namespace TestApplication.StructureMap.ContainerTests
{
    public class RegistryInstanceConfigure
    {
        public RegistryInstanceConfigure()
        {
            var registry = new global::StructureMap.Configuration.DSL.Registry();
            registry.For<IFoo>().Use<Foo>();
        }
    }
}