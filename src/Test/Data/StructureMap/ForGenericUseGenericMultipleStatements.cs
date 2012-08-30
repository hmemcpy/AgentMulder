using StructureMap;
using TestApplication.Types;

namespace TestApplication.StructureMap
{
    public class ForGenericUseGenericMultipleStatements
    {
        public ForGenericUseGenericMultipleStatements()
        {
            var container = new Container(x =>
            {
                x.For<IFoo>().Use<Foo>();
                x.For<IBar>().Use<Bar>();
            });
        } 
    }
}