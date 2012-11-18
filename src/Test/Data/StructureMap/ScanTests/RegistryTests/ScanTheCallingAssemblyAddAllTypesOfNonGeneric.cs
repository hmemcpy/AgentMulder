using StructureMap;
using StructureMap.Configuration.DSL;
using TestApplication.Types;

namespace TestApplication.StructureMap.ScanTests.RegistryTests
{
    public class ScanTheCallingAssemblyAddAllTypesOfNonGeneric : Registry
    {
        public ScanTheCallingAssemblyAddAllTypesOfNonGeneric()
        {
            Scan(scanner =>
            {
                scanner.TheCallingAssembly();
                scanner.AddAllTypesOf(typeof(ICommon));
            });
        } 
    }
}