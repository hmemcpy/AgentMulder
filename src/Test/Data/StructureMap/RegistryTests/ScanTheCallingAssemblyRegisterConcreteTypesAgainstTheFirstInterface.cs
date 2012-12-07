using SomeNamespace;
using StructureMap.Configuration.DSL;

namespace TestApplication.StructureMap.RegistryTests
{
    public class ScanTheCallingAssemblyRegisterConcreteTypesAgainstTheFirstInterface : Registry
    {
        public ScanTheCallingAssemblyRegisterConcreteTypesAgainstTheFirstInterface()
        {
            Scan(scanner =>
            {
                scanner.TheCallingAssembly();
                scanner.RegisterConcreteTypesAgainstTheFirstInterface();
            });
        } 
    }
}