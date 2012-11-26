using System.Reflection;
using StructureMap;
using TestApplication.Types;

namespace TestApplication.StructureMap.ScanTests
{
    public class ScanAssemblyGetExecutingAssembly
    {
        public ScanAssemblyGetExecutingAssembly()
        {
            var container = new Container(x => x.Scan(scanner =>
            {
                scanner.Assembly(Assembly.GetExecutingAssembly());
                scanner.WithDefaultConventions();
            }));

        } 
    }
}