// Patterns: 0

using Autofac;
using TestApplication.Types;

namespace TestApplication.Autofac
{
    public class RegisterNewObjectAsInterface : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            // This is considered a valid registration, but result in an exception at runtime.
            // Therefore, ignored by Agent Mulder
            builder.Register(context => new object()).As<IFoo>();
        }
    }
}