using TestApplication.Types;

namespace TestApplication.StructureMap.Container
{
    public class ForGenericUseGenericStatement
    {
        public ForGenericUseGenericStatement()
        {
            var container = new global::StructureMap.Container(x =>
            {
                x.For<IFoo>().Use<Foo>();
            });
        } 
    }
}