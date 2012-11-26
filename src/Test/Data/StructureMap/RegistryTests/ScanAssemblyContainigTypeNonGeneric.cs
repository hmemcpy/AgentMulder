using StructureMap.Configuration.DSL;
using TestApplication.Types;

namespace TestApplication.StructureMap.RegistryTests
{
    public class ScanAssemblyContainigTypeNonGeneric : Registry
    {
        public ScanAssemblyContainigTypeNonGeneric()
        {
            Scan(scanner =>
            {
                scanner.AssemblyContainingType(typeof(IFoo));
                scanner.WithDefaultConventions();
            });
        }
    }
}