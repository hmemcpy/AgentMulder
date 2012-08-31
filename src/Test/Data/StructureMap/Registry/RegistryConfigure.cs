using TestApplication.Types;

namespace TestApplication.StructureMap.Registry
{
    public class RegistryConfigure
    {
        public RegistryConfigure()
        {
            var registry = new global::StructureMap.Configuration.DSL.Registry();
            registry.For<IFoo>().Use<Foo>();
        }
    }
}