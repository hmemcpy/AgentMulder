using StructureMap.Configuration.DSL;

namespace TestApplication.StructureMap.RegistryTests
{
    public class ScanTheCallingAssemblySingleImplementationsOfInterface : Registry
    {
        public ScanTheCallingAssemblySingleImplementationsOfInterface()
        {
            Scan(scanner =>
            {
                scanner.TheCallingAssembly();
                scanner.SingleImplementationsOfInterface();
            });
        } 
    }
}