// Patterns: 1
// Matches: CommonImpl1.cs
// NotMatches: Foo.cs

using SimpleInjector;
using TestApplication.Types;

namespace TestApplication.SimpleInjector
{
    public class RegisterConcreteSingletonGeneric
    {
        public RegisterConcreteSingletonGeneric()
        {
            var container = new Container();

            container.RegisterSingle<CommonImpl1>();
        } 
    }
}