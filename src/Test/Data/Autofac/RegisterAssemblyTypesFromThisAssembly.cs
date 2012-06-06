using System.Reflection;
using Autofac;
using Module = Autofac.Module;

namespace TestApplication.Autofac
{
    public class RegisterAssemblyTypes : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(ThisAssembly);
        }
    }
}