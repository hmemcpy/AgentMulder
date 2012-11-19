using System;
using StructureMap.Configuration.DSL;
using TestApplication.Types;

namespace TestApplication.StructureMap.ScanTests.RegistryTests
{
    public class ScanAssemblyContainigTypeGeneric : Registry
    {
        public ScanAssemblyContainigTypeGeneric()
        {
            Scan(scanner =>
            {
                scanner.AssemblyContainingType<IFoo>();
                scanner.WithDefaultConventions();
            });
        }
    }
}