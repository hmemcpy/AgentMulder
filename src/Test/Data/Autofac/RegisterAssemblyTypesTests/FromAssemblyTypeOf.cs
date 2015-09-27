// Patterns: 1
// Matches: Foo.cs,Bar.cs,Baz,CommonImpl1.cs,Page.cs,GoldCard.cs
// NotMatches: 

using Autofac;
using TestApplication.Types;

namespace TestApplication.Autofac.RegisterAssemblyTypesTests
{
    public class FromAssemblyTypeOf : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(typeof(IFoo).Assembly);
        }
    }
}