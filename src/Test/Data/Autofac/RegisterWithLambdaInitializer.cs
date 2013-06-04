// Patterns: 1
// Matches: FooBar.cs
// NotMatches: Foo.cs

using Autofac;
using TestApplication.Types;

namespace TestApplication.Autofac
{
    public class RegisterWithLambdaInitializer : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.Register(context => new FooBar { Number = 5 });
        }
    }
}