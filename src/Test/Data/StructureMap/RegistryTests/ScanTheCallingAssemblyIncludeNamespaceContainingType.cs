using SomeNamespace;
using StructureMap;
using StructureMap.Configuration.DSL;

namespace TestApplication.StructureMap.RegistryTests
{
    public class ScanTheCallingAssemblyIncludeNamespaceContainingType : Registry
    {
        public ScanTheCallingAssemblyIncludeNamespaceContainingType()
        {
            Scan(scanner =>
            {
                scanner.TheCallingAssembly();
                scanner.IncludeNamespaceContainingType<InSomeNamespace>();
                scanner.WithDefaultConventions();
            });
        } 
    }
}