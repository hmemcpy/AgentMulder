using Autofac;
using TestApplication.Types;

namespace TestApplication.Autofac.RegisterAssemblyTypesTests
{
    public class AssignableToNonGeneric1 : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(ThisAssembly).AssignableTo(typeof(CreditCard));
        }
    }
}