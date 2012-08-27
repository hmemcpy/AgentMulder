using StructureMap;
using StructureMap.Configuration.DSL.Expressions;
using TestApplication.Types;

namespace TestApplication.StructureMap.ConfigurationExpression
{
    public class ForGenericUseGeneric
    {
        public ForGenericUseGeneric()
        {
            var container = new Container(x =>
            {
                x.For<IFoo>().Use<Foo>();
            });
        }
    }
}