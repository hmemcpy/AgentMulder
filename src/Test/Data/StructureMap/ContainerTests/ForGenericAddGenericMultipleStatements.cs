// Patterns: 2
// Matches: Foo.cs,Bar.cs
// NotMatches: Baz.cs

using StructureMap;
using TestApplication.Types;

namespace TestApplication.StructureMap.ContainerTests
{
    public class ForGenericAddGenericMultipleStatements
    {
        public ForGenericAddGenericMultipleStatements()
        {
            var container = new Container(x =>
            {
                x.For<IFoo>().Add<Foo>();
                x.For<IBar>().Add<Bar>();
            });
        } 
    }
}