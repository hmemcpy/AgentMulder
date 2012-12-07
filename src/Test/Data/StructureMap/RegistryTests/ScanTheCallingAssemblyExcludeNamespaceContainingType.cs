using StructureMap;
using StructureMap.Configuration.DSL;
using TestApplication.Types;

namespace TestApplication.StructureMap.RegistryTests
{
    public class ScanTheCallingAssemblyExcludeNamespaceContainingType : Registry
    {
        public ScanTheCallingAssemblyExcludeNamespaceContainingType()
        {
            Scan(scanner =>
            {
                scanner.TheCallingAssembly();
                scanner.ExcludeNamespaceContainingType<Foo>();
                scanner.WithDefaultConventions();
            });
        } 
    }
}