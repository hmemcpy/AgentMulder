using StructureMap.Configuration.DSL;
using TestApplication.Types;

namespace TestApplication.StructureMap.RegistryTests
{
    public class ForGenericUseGenericWithAdditionalParams : Registry
    {
        public ForGenericUseGenericWithAdditionalParams()
        {
            For<IFoo>().Singleton().InterceptWith(null).Use<Foo>();
        }
    }
}