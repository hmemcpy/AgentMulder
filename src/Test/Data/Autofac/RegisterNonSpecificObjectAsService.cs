// Patterns: 1
// Matches: Foo.cs
// NotMatches: Bar.cs

using Autofac;
using TestApplication.Types;

namespace TestApplication.Autofac
{
    public class RegisterNonSpecificObjectAsService : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.Register(context => (object)(new Foo())).As<IFoo>();
        }
    }
}