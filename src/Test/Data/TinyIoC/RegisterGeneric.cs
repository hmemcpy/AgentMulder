// Patterns: 1
// Matches: Foo.cs
// NotMatches: Bar.cs

using TestApplication.Types;
using TinyIoC;

namespace TestApplication.TinyIoC
{
    public class RegisterGeneric
    {
        public RegisterGeneric()
        {
            var container = TinyIoCContainer.Current;

            container.Register<Foo>();
        }
    }
}