// Patterns: 1
// Matches: InSomeNamespace.cs,InSomeOtherNamespace.cs
// NotMatches: Foo.cs

using Autofac;

namespace TestApplication.Autofac.RegisterAssemblyTypesTests
{
    public class InNamespaceString : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(ThisAssembly).InNamespace("SomeNamespace");
        }
    }
}