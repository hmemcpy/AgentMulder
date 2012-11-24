using StructureMap.Configuration.DSL;

namespace TestApplication.StructureMap.RegistryTests
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