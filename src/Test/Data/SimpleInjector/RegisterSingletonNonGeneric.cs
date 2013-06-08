// Patterns: 1
// Matches: CommonImpl1.cs
// NotMatches: Foo.cs

using SimpleInjector;
using TestApplication.Types;

namespace TestApplication.SimpleInjector
{
    public class RegisterSingletonNonGeneric
    {
        public RegisterSingletonNonGeneric()
        {
            var container = new Container();

            container.RegisterSingle(typeof(ICommon), typeof(CommonImpl1));
        } 
    }
}