using StructureMap.Configuration.DSL;
using TestApplication.Types;

namespace TestApplication.StructureMap.RegistryTests
{
    public class ForNonGenericUseNonGenericType : Registry
    {
        public ForNonGenericUseNonGenericType()
        {
            For(typeof(IFoo)).Use(typeof(Foo));
        }
    }
}