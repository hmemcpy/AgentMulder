using StructureMap;
using StructureMap.Configuration.DSL;
using TestApplication.Types;

namespace TestApplication.StructureMap.RegistryTests
{
    public class ScanAssemblyTypeofTAssembly : Registry
    {
        public ScanAssemblyTypeofTAssembly()
        {
            Scan(scanner =>
            {
                scanner.Assembly(typeof(IFoo).Assembly);
                scanner.WithDefaultConventions();
            });
        } 
    }
}