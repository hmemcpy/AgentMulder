using StructureMap;

namespace TestApplication.StructureMap.ScanTests
{
    public class ScanTheCallingAssemblyWithDefaultConventions
    {
        public ScanTheCallingAssemblyWithDefaultConventions()
        {
            var container = new Container(x => x.Scan(scanner =>
            {
                scanner.TheCallingAssembly();
                scanner.WithDefaultConventions();
            }));
        } 
    }
}