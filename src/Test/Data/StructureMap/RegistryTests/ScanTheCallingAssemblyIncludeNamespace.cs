using StructureMap;
using StructureMap.Configuration.DSL;

namespace TestApplication.StructureMap.RegistryTests
{
    public class ScanTheCallingAssemblyIncludeNamespace : Registry
    {
        public ScanTheCallingAssemblyIncludeNamespace()
        {
            Scan(scanner =>
            {
                scanner.TheCallingAssembly();
                scanner.IncludeNamespace("SomeNamespace");
                scanner.WithDefaultConventions();
            });
        } 
    }
}