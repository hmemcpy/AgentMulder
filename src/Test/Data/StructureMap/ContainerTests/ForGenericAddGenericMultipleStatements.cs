using StructureMap;
using TestApplication.Types;

namespace TestApplication.StructureMap.ContainerTests
{
    public class ForGenericAddGenericMultipleStatements
    {
        public ForGenericAddGenericMultipleStatements()
        {
            var container = new Container(x =>
            {
                x.For<IFoo>().Add<Foo>();
                x.For<IBar>().Add<Bar>();
            });
        } 
    }
}