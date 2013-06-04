// Patterns: 0

using Autofac;
using TestApplication.Types;

namespace TestApplication.Autofac
{
    public class RegisterNewObject : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.Register(context => new object());
        }
    }
}