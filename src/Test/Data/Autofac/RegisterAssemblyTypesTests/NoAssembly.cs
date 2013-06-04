// Patterns: 0

using Autofac;

namespace TestApplication.Autofac.RegisterAssemblyTypesTests
{
    public class NoAssembly : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            // no types will be registered
            builder.RegisterAssemblyTypes();
        }
    }
}