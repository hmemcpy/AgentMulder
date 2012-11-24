using StructureMap;

namespace TestApplication.StructureMap.ScanTests
{
    public class ScanTheCallingAssemblyIncludeNamespace
    {
        public ScanTheCallingAssemblyIncludeNamespace()
        {
            var container = new Container(x => x.Scan(scanner =>
            {
                scanner.TheCallingAssembly();
                scanner.IncludeNamespace("SomeNamespace");
                scanner.WithDefaultConventions();
            }));
        } 
    }
}