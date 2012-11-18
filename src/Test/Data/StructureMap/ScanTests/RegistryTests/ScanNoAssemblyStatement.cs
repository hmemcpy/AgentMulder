using StructureMap;
using StructureMap.Configuration.DSL;

namespace TestApplication.StructureMap.ScanTests.RegistryTests
{
    public class ScanNoAssemblyStatement : Registry
    {
        public ScanNoAssemblyStatement()
        {
            Scan(scanner => 
            {
                // adding as a sanity, should yield no results

                // ReSharper disable ConvertToLambdaExpression
                scanner.Include(type => true);
                // ReSharper restore ConvertToLambdaExpression
            });
        } 
    }
}