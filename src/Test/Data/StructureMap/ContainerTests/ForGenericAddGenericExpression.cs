// Patterns: 1
// Matches: Foo.cs
// NotMatches: Bar.cs 

using StructureMap;
using TestApplication.Types;

namespace TestApplication.StructureMap.ContainerTests
{
    public class ForGenericAddGenericExpression
    {
        public ForGenericAddGenericExpression()
        {
            var container = new Container(x => x.For<IFoo>().Add<Foo>());
        } 
    }
}