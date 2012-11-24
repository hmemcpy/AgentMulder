using StructureMap;
using StructureMap.Configuration.DSL;

namespace TestApplication.StructureMap.RegistryTests
{
    public class ScanTheCallingAssemblyExcludeNamespace : Registry
    {
        public ScanTheCallingAssemblyExcludeNamespace()
        {
            Scan(scanner =>
            {
                scanner.TheCallingAssembly();
                scanner.ExcludeNamespace("TestApplication.Types");
                scanner.WithDefaultConventions();
            });
        } 
    }
}