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