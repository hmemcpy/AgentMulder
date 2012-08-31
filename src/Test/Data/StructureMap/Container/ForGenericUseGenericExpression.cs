using TestApplication.Types;

namespace TestApplication.StructureMap.Container
{
    public class ForGenericUseGenericExpression
    {
        public ForGenericUseGenericExpression()
        {
            var container = new global::StructureMap.Container(x => x.For<IFoo>().Use<Foo>());
        } 
    }
}