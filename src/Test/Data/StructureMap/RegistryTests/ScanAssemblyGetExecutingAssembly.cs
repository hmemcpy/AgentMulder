using System.Reflection;
using StructureMap;
using StructureMap.Configuration.DSL;

namespace TestApplication.StructureMap.RegistryTests
{
    public class ScanAssemblyGetExecutingAssembly : Registry
    {
        public ScanAssemblyGetExecutingAssembly()
        {
            Scan(scanner =>
            {
                scanner.Assembly(Assembly.GetExecutingAssembly());
                scanner.WithDefaultConventions();
            });

        } 
    }
}