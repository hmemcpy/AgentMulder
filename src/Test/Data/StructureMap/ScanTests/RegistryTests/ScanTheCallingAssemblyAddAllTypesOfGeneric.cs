using StructureMap;
using StructureMap.Configuration.DSL;
using TestApplication.Types;

namespace TestApplication.StructureMap.ScanTests.RegistryTests
{
    public class ScanTheCallingAssemblyAddAllTypesOfGeneric : Registry
    {
        public ScanTheCallingAssemblyAddAllTypesOfGeneric()
        {
            Scan(scanner =>
            {
                scanner.TheCallingAssembly();
                scanner.AddAllTypesOf<ICommon>();
            });
        } 
    }
}