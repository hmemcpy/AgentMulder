// Patterns: 1
// Matches: Foo.cs,Bar.cs
// NotMatches: CommonImpl1.cs

using System.Reflection;
using StructureMap.Configuration.DSL;

namespace TestApplication.StructureMap.ScanTests
{
    public class RegistryScanAssemblyGetExecutingAssembly : Registry
    {
        public RegistryScanAssemblyGetExecutingAssembly()
        {
            Scan(scanner =>
            {
                scanner.Assembly(Assembly.GetExecutingAssembly());
                scanner.WithDefaultConventions();
            });
        }
    }
}