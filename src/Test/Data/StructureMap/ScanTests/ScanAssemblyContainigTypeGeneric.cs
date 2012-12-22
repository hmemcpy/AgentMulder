// Patterns: 1
// Matches: Foo.cs,Bar.cs
// NotMatches: CommonImpl1.cs

using System;
using StructureMap;
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