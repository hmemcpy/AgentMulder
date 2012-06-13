using Autofac;
using TestApplication.Types;

namespace TestApplication.Autofac.RegisterAssemblyTypesTests
{
    public class ExceptWithArgument : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(ThisAssembly).Except<GoldCard>(registrationBuilder => { });
        }
    }
}