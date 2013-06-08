// Patterns: 1
// Matches: CommonImpl1.cs
// NotMatches: Foo.cs

using SimpleInjector;
using TestApplication.Types;

namespace TestApplication.SimpleInjector
{
    public class RegisterTransientGeneric
    {
        public RegisterTransientGeneric()
        {
            var container = new Container();

            container.Register<ICommon, CommonImpl1>();
        } 
    }
}