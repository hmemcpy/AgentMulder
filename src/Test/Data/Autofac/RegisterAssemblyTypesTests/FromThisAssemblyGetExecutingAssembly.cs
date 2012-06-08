using System.Reflection;
using Autofac;
using Module = Autofac.Module;

namespace TestApplication.Autofac.RegisterAssemblyTypesTests
{
    public class FromThisAssemblyGetExecutingAssembly : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(Assembly.GetExecutingAssembly());
        }
    }
}