using StructureMap;
using TestApplication.Types;

namespace TestApplication.StructureMap
{
    public class ForGenericUseGenericWithAdditionalParams
    {
        public ForGenericUseGenericWithAdditionalParams()
        {
            var container = new Container(x => 
                x.For<IFoo>().Singleton().InterceptWith(null).Use<Foo>()
                );
        }
    }
}