using Autofac;
using Module = Autofac.Module;

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