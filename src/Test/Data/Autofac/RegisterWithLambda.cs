using Autofac;
using TestApplication.Types;

namespace TestApplication.Autofac
{
    public class RegisterWithLambda : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.Register(context => new Foo());
        }
    }
}