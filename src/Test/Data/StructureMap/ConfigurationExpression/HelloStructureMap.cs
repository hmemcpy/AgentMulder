using StructureMap;
using TestApplication.Types;

namespace TestApplication.StructureMap.ConfigurationExpression
{
    public class HelloStructureMap
    {
        public HelloStructureMap()
        {
            var container = new Container(x => {
                x.For<IFoo>().Use<Foo>();
            });
        }
    }
}