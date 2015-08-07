// Patterns: 1
// Matches: Foo.cs,Bar.cs,Baz,CommonImpl1.cs,Page.cs,GoldCard.cs
// NotMatches: 

using Autofac;

namespace TestApplication.Autofac.RegisterAssemblyTypesTests
{
    public class FromThisAssemblyModuleProperty : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(ThisAssembly);
        }
    }
}