using StructureMap;
using TestApplication.Types;

namespace TestApplication.StructureMap
{
    public class ForGenericUseGenericStatement
    {
        public ForGenericUseGenericStatement()
        {
            var container = new Container(x =>
            {
                x.For<IFoo>().Use<Foo>();
            });
        } 
    }
}