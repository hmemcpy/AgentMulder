using StructureMap;

namespace TestApplication.StructureMap.ScanTests
{
    public class ScanTheCallingAssembly
    {
        public ScanTheCallingAssembly()
        {
            var container = new Container(x => x.Scan(scanner =>
            {
                // without specifying conventions, this should yield no results

                // ReSharper disable ConvertToLambdaExpression
                scanner.TheCallingAssembly();
                // ReSharper restore ConvertToLambdaExpression
            }));
        }

    }
}