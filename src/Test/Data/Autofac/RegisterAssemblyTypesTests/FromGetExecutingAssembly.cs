// Patterns: 1
// Matches: Foo.cs,Bar.cs,Baz,CommonImpl1.cs,Page.cs,GoldCard.cs
// NotMatches: 

using System.Reflection;
using Autofac;
using Module = Autofac.Module;

namespace TestApplication.Autofac.RegisterAssemblyTypesTests
{
    public class FromGetExecutingAssembly : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(Assembly.GetExecutingAssembly());
        }
    }
}