using StructureMap.Configuration.DSL;
using TestApplication.Types;

namespace TestApplication.StructureMap.RegistryTests
{
    public class ForGenericUseGeneric : Registry
    {
        public ForGenericUseGeneric()
        {
            For<IFoo>().Use<Foo>();
        }
    }
}