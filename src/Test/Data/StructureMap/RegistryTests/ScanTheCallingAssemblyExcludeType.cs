using StructureMap;
using StructureMap.Configuration.DSL;
using TestApplication.Types;

namespace TestApplication.StructureMap.RegistryTests
{
    public class ScanTheCallingAssemblyExcludeType : Registry
    {
        public ScanTheCallingAssemblyExcludeType()
        {
            Scan(scanner =>
            {
                scanner.TheCallingAssembly();
                scanner.ExcludeType<Bar>();
                scanner.WithDefaultConventions();
            });
        } 
    }
}