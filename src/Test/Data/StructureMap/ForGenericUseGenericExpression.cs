using StructureMap;
using TestApplication.Types;

namespace TestApplication.StructureMap
{
    public class ForGenericUseGenericExpression
    {
        public ForGenericUseGenericExpression()
        {
            var container = new Container(x => x.For<IFoo>().Use<Foo>());
        } 
    }
}