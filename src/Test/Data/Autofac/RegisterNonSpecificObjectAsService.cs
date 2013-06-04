// Patterns: 0

using Autofac;
using TestApplication.Types;

namespace TestApplication.Autofac
{
    public class RegisterNonSpecificObjectAsService : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            // Casting an instance to object is not supported
            builder.Register(context => (object)(new Foo())).As<IFoo>();
        }
    }
}