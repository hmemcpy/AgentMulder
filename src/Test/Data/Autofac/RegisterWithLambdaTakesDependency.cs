// Patterns: 1
// Matches: TakesDependency.cs
// NotMatches: Foo.cs

using Autofac;
using TestApplication.Types;

namespace TestApplication.Autofac
{
    public class RegisterWithLambdaTakesDependency : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.Register(context => new TakesDependency(context.Resolve<IFoo>()));
        }
    }
}