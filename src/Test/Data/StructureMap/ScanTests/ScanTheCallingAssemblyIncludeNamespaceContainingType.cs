using SomeNamespace;
using StructureMap;

namespace TestApplication.StructureMap.ScanTests
{
    public class ScanTheCallingAssemblyIncludeNamespaceContainingType
    {
        public ScanTheCallingAssemblyIncludeNamespaceContainingType()
        {
            var container = new Container(x => x.Scan(scanner =>
            {
                scanner.TheCallingAssembly();
                scanner.IncludeNamespaceContainingType<InSomeNamespace>();
                scanner.WithDefaultConventions();
            }));
        } 
    }
}