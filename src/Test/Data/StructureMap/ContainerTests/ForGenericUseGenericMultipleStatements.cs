using TestApplication.Types;

namespace TestApplication.StructureMap.ContainerTests
{
    public class ForGenericUseGenericMultipleStatements
    {
        public ForGenericUseGenericMultipleStatements()
        {
            var container = new global::StructureMap.Container(x =>
            {
                x.For<IFoo>().Use<Foo>();
                x.For<IBar>().Use<Bar>();
            });
        } 
    }
}