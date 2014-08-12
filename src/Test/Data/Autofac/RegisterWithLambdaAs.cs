// Patterns: 1
// Matches: Foo.cs
// NotMatches: Bar.cs

using Autofac;
using TestApplication.Types;

namespace TestApplication.Autofac
{
    public class RegisterWithLambdaAs : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            // ReSharper disable once RedundantCast
            builder.Register(context => new Foo() as Foo);
        }
    }
}