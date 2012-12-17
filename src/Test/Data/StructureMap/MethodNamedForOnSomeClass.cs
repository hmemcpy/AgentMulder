// Patterns: 0

using TestApplication.Types;

namespace TestApplication.StructureMap
{
    public class MethodNamedForOnSomeClass
    {
        public MethodNamedForOnSomeClass()
        {
            var someClass = new SomeClass();
            someClass.For<ICommon>().Use<CommonImpl1>();
        }
    }
}