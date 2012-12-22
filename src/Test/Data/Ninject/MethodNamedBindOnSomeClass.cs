// Patterns: 0

using TestApplication.Types;

namespace TestApplication.Ninject
{
    public class MethodNamedBindOnSomeClass
    {
        public MethodNamedBindOnSomeClass()
        {
            var someClass = new SomeClass();
            someClass.Bind<ICommon>().To<CommonImpl1>();
        }
    }
}