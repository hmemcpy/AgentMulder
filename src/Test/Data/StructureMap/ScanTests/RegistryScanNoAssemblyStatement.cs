// Patterns: 0

using StructureMap.Configuration.DSL;

namespace TestApplication.StructureMap.ScanTests
{
    public class RegistryScanNoAssemblyStatement : Registry
    {
        public RegistryScanNoAssemblyStatement()
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