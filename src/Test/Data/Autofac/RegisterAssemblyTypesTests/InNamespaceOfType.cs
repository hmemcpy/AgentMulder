// Patterns: 1
// Matches: InSomeNamespace.cs,InSomeOtherNamespace.cs
// NotMatches: Foo.cs

using Autofac;
using SomeNamespace;

namespace TestApplication.Autofac.RegisterAssemblyTypesTests
{
    public class InNamespaceOfType : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(ThisAssembly).InNamespaceOf<InSomeNamespace>();
        }
    }
}