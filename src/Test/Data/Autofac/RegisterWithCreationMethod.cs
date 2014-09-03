// Patterns: 1
// Matches: Foo.cs
// NotMatches: Bar.cs

using Autofac;
using TestApplication.Types;

namespace TestApplication.Autofac
{
    public class RegisterWithCreationMethod : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.Register(c => CreateInstance());
        }

        private static Foo CreateInstance()
        {
            return new Foo();
        }
    }
}