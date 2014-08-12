// Patterns: 1
// Matches: Foo.cs
// NotMatches: Bar.cs

using Autofac;
using TestApplication.Types;

namespace TestApplication.Autofac
{
    public class RegisterWithLambdaMethodCall : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.Register(context => Foo());
        }

        private Foo Foo()
        {
            return new Foo();
        }
    }
}