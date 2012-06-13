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