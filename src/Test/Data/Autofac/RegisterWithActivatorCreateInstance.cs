// Patterns: 0

using System;
using Autofac;
using TestApplication.Types;

namespace TestApplication.Autofac
{
    public class RegisterWithActivatorCreateInstance : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.Register(context => Activator.CreateInstance(typeof(Foo)));
        }
    }
}