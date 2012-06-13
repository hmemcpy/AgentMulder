using Autofac;
using TestApplication.Types;

namespace TestApplication.Autofac
{
    public class RegisterWithLambdaInitializer : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.Register(context => new FooBar { Number = 5 });
        }
    }
}