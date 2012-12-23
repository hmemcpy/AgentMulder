// Patterns: 1
// Matches: Foo.cs
// NotMatches: IFoo.cs

using System;
using Autofac;
using TestApplication.Types;

namespace TestApplication.Autofac
{
    public class RegisterNonSpecificObject : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.Register(context => (object)(new Foo()));
        }
    }
}