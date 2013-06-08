// Patterns: 1
// Matches: CommonImpl1.cs
// NotMatches: Foo.cs

using SimpleInjector;
using SimpleInjector.Extensions;
using TestApplication.Types;

namespace TestApplication.SimpleInjector
{
    public class RegisterSingleDecorator
    {
        public RegisterSingleDecorator()
        {
            var container = new Container();

            container.RegisterSingleDecorator(typeof(ICommon), typeof(CommonImpl1));
        } 
    }
}