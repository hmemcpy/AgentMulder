// Patterns: 2
// Matches: Foo.cs,Bar.cs
// NotMatches: Baz.cs 

using StructureMap;
using TestApplication.Types;

namespace TestApplication.StructureMap.ContainerTests
{
    public class ForGenericUseGenericMultipleStatements
    {
        public ForGenericUseGenericMultipleStatements()
        {
            var container = new Container(x =>
            {
                x.For<IFoo>().Use<Foo>();
                x.For<IBar>().Use<Bar>();
            });
        } 
    }
}