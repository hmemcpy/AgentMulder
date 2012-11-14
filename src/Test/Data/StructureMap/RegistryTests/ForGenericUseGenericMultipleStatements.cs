using StructureMap.Configuration.DSL;
using TestApplication.Types;

namespace TestApplication.StructureMap.RegistryTests
{
    public class ForGenericUseGenericMultipleStatements : Registry
    {
        public ForGenericUseGenericMultipleStatements()
        {
            For<IFoo>().Use<Foo>();
            For<IBar>().Use<Bar>();            
        }
    }
}