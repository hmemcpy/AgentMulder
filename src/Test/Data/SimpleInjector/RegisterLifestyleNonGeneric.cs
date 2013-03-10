// Patterns: 1
// Matches: CommonImpl1.cs
// NotMatches: Foo.cs

using SimpleInjector;
using TestApplication.Types;

namespace TestApplication.SimpleInjector
{
    public class RegisterLifestyleNonGeneric
    {
        public RegisterLifestyleNonGeneric()
        {
            var container = new Container();

            Lifestyle custom = null;

            container.Register(typeof(ICommon), typeof(CommonImpl1), custom);
        } 
    }
}