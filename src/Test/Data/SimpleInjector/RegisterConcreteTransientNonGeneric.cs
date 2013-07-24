// Patterns: 1
// Matches: CommonImpl1.cs
// NotMatches: Foo.cs

using SimpleInjector;
using TestApplication.Types;

namespace TestApplication.SimpleInjector
{
    public class RegisterConcreteTransientNonGeneric
    {
        public RegisterConcreteTransientNonGeneric()
        {
            var container = new Container();

            container.Register(typeof(CommonImpl1));
        } 
    }
}