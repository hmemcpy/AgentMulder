// Patterns: 0

using StructureMap;

namespace TestApplication.StructureMap
{
    // THIS FILE IS NOT BEING COMPILED, AND IS SET TO BE IGNORED BY RESHARPER
    // this file tests what happens if the plugin tries to analyze unresolved types
    public class BrokenReference
    {
        public BrokenReference()
        {
            var container = new Container(x =>
            {
                x.For<IDontExist>().Use<DontExistImplementation>();
            });
        }
    }
}