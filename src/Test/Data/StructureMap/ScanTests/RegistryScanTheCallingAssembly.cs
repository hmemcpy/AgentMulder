// Patterns: 0

using StructureMap.Configuration.DSL;

namespace TestApplication.StructureMap.ScanTests
{
    public class RegistryScanTheCallingAssembly : Registry
    {
        public RegistryScanTheCallingAssembly()
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