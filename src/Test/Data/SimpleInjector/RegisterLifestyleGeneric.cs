// Patterns: 1
// Matches: CommonImpl1.cs
// NotMatches: Foo.cs

using SimpleInjector;
using TestApplication.Types;

namespace TestApplication.SimpleInjector
{
    public class RegisterLifestyleGeneric
    {
        public RegisterLifestyleGeneric()
        {
            var container = new Container();

            container.Register<ICommon, CommonImpl1>(Lifestyle.Transient);
        } 
    }
}