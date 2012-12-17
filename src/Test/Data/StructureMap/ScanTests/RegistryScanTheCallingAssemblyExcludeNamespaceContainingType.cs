// Patterns: 1
// Matches: InSomeNamespace.cs,InSomeOtherNamespace.cs
// NotMatches: CommonImpl1.cs,Foo.cs

using StructureMap.Configuration.DSL;
using TestApplication.Types;

namespace TestApplication.StructureMap.ScanTests
{
    public class RegistryScanTheCallingAssemblyExcludeNamespaceContainingType : Registry
    {
        public RegistryScanTheCallingAssemblyExcludeNamespaceContainingType()
        {
            Scan(scanner =>
            {
                scanner.TheCallingAssembly();
                scanner.ExcludeNamespaceContainingType<Foo>();
                scanner.WithDefaultConventions();
            });
        }
    }
}