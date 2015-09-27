// Patterns: 1
// Matches: Foo.cs
// NotMatches: Bar.cs 

using StructureMap;
using TestApplication.Types;

namespace TestApplication.StructureMap.ContainerTests
{
    public class ForGenericUseGenericStatement
    {
        public ForGenericUseGenericStatement()
        {
            // ReSharper disable ConvertToLambdaExpression
            var container = new Container(x =>
            {
                x.For<IFoo>().Use<Foo>();
            });
            // ReSharper restore ConvertToLambdaExpression
        } 
    }
}