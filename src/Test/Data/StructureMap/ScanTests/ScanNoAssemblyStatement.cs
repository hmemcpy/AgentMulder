using StructureMap;

namespace TestApplication.StructureMap.ScanTests
{
    public class ScanNoAssemblyStatement
    {
        public ScanNoAssemblyStatement()
        {
            var container = new Container(x => x.Scan(scanner => 
            {
                // adding as a sanity, should yield no results

                // ReSharper disable ConvertToLambdaExpression
                scanner.Include(type => true);
                // ReSharper restore ConvertToLambdaExpression
            }));
        } 
    }
}