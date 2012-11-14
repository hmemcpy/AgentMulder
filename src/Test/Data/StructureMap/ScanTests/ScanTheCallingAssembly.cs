using StructureMap;

namespace TestApplication.StructureMap.ScanTests
{
    public class ScanTheCallingAssembly
    {
        public ScanTheCallingAssembly()
        {
            var container = new Container(x => x.Scan(scanner =>
            {
                // this registration should yield no results, adding as a sanity
                scanner.TheCallingAssembly();
            }));
        } 
 
    }
}