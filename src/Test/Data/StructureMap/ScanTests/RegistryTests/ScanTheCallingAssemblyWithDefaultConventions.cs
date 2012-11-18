using StructureMap;
using StructureMap.Configuration.DSL;

namespace TestApplication.StructureMap.ScanTests.RegistryTests
{
    public class ScanTheCallingAssemblyWithDefaultConventions : Registry
    {
        public ScanTheCallingAssemblyWithDefaultConventions()
        {
            Scan(scanner =>
            {
                scanner.TheCallingAssembly();
                scanner.WithDefaultConventions();
            });
        } 
    }
}