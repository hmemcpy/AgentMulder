using System.Reflection;
using Autofac;
using TestApplication.Types;
using Module = Autofac.Module;

namespace TestApplication.Autofac.RegisterAssemblyTypesTests
{
    public class AllThreeTogether : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(ThisAssembly, Assembly.GetExecutingAssembly(), typeof(IFoo).Assembly);
        }
    }
}