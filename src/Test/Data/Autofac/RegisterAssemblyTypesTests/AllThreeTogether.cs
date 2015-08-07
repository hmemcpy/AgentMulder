// Patterns: 3
// Matches: Foo.cs,Bar.cs,Baz,CommonImpl1.cs,Page.cs
// NotMatches: 

using System.Reflection;
using Autofac;
using TestApplication.Types;
using Module = Autofac.Module;

namespace TestApplication.Autofac.RegisterAssemblyTypesTests
{
    public class AllThreeTogether : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(ThisAssembly, Assembly.GetExecutingAssembly(), typeof(IFoo).Assembly);
        }
    }
}