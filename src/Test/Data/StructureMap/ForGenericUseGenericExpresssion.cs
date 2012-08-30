using StructureMap;
using TestApplication.Types;

namespace TestApplication.StructureMap
{
    public class ForGenericUseGenericExpresssion
    {
        public ForGenericUseGenericExpresssion()
        {
            var container = new Container(x => x.For<IFoo>().Use<Foo>());
        } 
    }
}