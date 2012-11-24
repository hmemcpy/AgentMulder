using System.Reflection;
using StructureMap;

namespace TestApplication.StructureMap.ScanTests
{
    public class ScanTheCallingAssemblyExcludeNamespace
    {
        public ScanTheCallingAssemblyExcludeNamespace()
        {
            var container = new Container(x => x.Scan(scanner =>
            {
                scanner.TheCallingAssembly();
                scanner.ExcludeNamespace("TestApplication.Types");
                scanner.WithDefaultConventions();
            }));
        } 
    }
}