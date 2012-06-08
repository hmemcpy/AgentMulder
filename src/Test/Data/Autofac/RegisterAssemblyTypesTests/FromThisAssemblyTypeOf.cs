using System.Reflection;
using Autofac;
using TestApplication.Types;
using Module = Autofac.Module;

namespace TestApplication.Autofac.RegisterAssemblyTypesTests
{
    public class FromThisAssemblyTypeOf : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(typeof(IFoo).Assembly);
        }
    }
}