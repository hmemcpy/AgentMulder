using StructureMap;
using StructureMap.Configuration.DSL;
using TestApplication.Types;

namespace TestApplication.StructureMap.ScanTests
{
    public class ScanAssemblyContainigTypeNonGeneric
    {
        public ScanAssemblyContainigTypeNonGeneric()
        {
            var container = new Container(x => x.Scan(scanner =>
            {
                scanner.AssemblyContainingType(typeof(IFoo));
                scanner.WithDefaultConventions();
            }));
        }
    }
}