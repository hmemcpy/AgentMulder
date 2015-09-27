// Patterns: 1
// Matches: Foo.cs,Bar.cs,Baz,CommonImpl1.cs
// NotMatches: Page.cs,GoldCard.cs,TakesDependency.cs

using Autofac;

namespace TestApplication.Autofac.RegisterAssemblyTypesTests
{
    public class AsImplementedInterfaces : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(ThisAssembly).AsImplementedInterfaces();
        }
    }
}