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