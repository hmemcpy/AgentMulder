using StructureMap;
using StructureMap.Configuration.DSL;

namespace TestApplication.StructureMap.ScanTests.RegistryTests
{
    public class ScanTheCallingAssembly : Registry
    {
        public ScanTheCallingAssembly()
        {
            Scan(scanner =>
            {
                // without specifying conventions, this should yield no results

                // ReSharper disable ConvertToLambdaExpression
                scanner.TheCallingAssembly();
                // ReSharper restore ConvertToLambdaExpression
            });
        }

    }
}