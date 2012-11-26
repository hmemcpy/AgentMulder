using System;
using StructureMap;
using StructureMap.Configuration.DSL;
using TestApplication.Types;

namespace TestApplication.StructureMap.ScanTests
{
    public class ScanAssemblyContainigTypeGeneric
    {
        public ScanAssemblyContainigTypeGeneric()
        {
            var container = new Container(x => x.Scan(scanner =>
            {
                scanner.AssemblyContainingType<IFoo>();
                scanner.WithDefaultConventions();
            }));
        }
    }
}