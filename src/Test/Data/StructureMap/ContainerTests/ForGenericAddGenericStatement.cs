// Patterns: 1
// Matches: Foo.cs
// NotMatches: Bar.cs 

using StructureMap;
using TestApplication.Types;

namespace TestApplication.StructureMap.ContainerTests
{
    public class ForGenericAddGenericStatement
    {
        public ForGenericAddGenericStatement()
        {
            // ReSharper disable ConvertToLambdaExpression
            var container = new Container(x =>
            {
                x.For<IFoo>().Add<Foo>();
            });
            // ReSharper restore ConvertToLambdaExpression
        }
    }
}