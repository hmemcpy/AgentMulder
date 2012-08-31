using TestApplication.Types;

namespace TestApplication.StructureMap.ContainerTests
{
    public class ForGenericUseGenericWithAdditionalParams
    {
        public ForGenericUseGenericWithAdditionalParams()
        {
            var container = new global::StructureMap.Container(x => 
                x.For<IFoo>().Singleton().InterceptWith(null).Use<Foo>()
                );
        }
    }
}