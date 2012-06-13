using Autofac;
using TestApplication.Types;

namespace TestApplication.Autofac.RegisterAssemblyTypesTests
{
    public class AsNonGeneric1 : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(ThisAssembly).As(typeof(ICommon));
        }
    }
}