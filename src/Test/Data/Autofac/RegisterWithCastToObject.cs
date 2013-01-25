// Patterns: 0

using System;
using Autofac;
using TestApplication.Types;

namespace TestApplication.Autofac
{
    public class RegisterWithCastToObject : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.Register(context => (object)(new Foo()));
        }
    }
}