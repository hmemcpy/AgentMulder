using StructureMap;

namespace TestApplication.StructureMap.ScanTests
{
    public class ScanTheCallingAssembly
    {
        public ScanTheCallingAssembly()
        {
            var container = new Container(x => x.Scan(scanner =>
            {
                scanner.TheCallingAssembly();
            }));
        } 
 
    }
}