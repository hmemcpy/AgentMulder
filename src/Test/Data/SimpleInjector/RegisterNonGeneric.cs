// Patterns: 1
// Matches: CommonImpl1.cs,CommonImpl2.cs
// NotMatches: Foo.cs

using SimpleInjector;
using TestApplication.Types;

namespace TestApplication.SimpleInjector
{
    public class RegisterAllNonGeneric
    {
        public RegisterAllNonGeneric()
        {
            var container = new Container();

            container.RegisterAll(typeof(ICommon), new[] { typeof(CommonImpl1), typeof(CommonImpl2) });
        } 
    }
}