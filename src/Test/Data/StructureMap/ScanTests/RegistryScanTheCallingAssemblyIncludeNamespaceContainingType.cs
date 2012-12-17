// Patterns: 1
// Matches: InSomeNamespace.cs,InSomeOtherNamespace.cs
// NotMatches: CommonImpl1.cs,Foo.cs

using SomeNamespace;
using StructureMap.Configuration.DSL;

namespace TestApplication.StructureMap.ScanTests
{
    public class RegistryScanTheCallingAssemblyIncludeNamespaceContainingType : Registry
    {
        public RegistryScanTheCallingAssemblyIncludeNamespaceContainingType()
        {
            Scan(scanner =>
            {
                scanner.TheCallingAssembly();
                scanner.IncludeNamespaceContainingType<InSomeNamespace>();
                scanner.WithDefaultConventions();
            });
        }
    }
}