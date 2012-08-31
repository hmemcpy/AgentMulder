using TestApplication.Types;

namespace TestApplication.StructureMap.ContainerTests
{
    public class ForNonGenericUseNonGenericType
    {
        public ForNonGenericUseNonGenericType()
        {
            var container = new global::StructureMap.Container(x => x.For(typeof(IFoo)).Use(typeof(Foo)));
        } 
    }
}