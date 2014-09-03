// Patterns: 1
// Matches: MyMvcController.cs
// NotMatches: Foo.cs

using Autofac;
using Autofac.Integration.Mvc;
using TestApplication.Types;

namespace TestApplication.Autofac.Mvc
{
    public class RegisterControllers
    {
        public RegisterControllers()
        {
            var builder = new ContainerBuilder();

            builder.RegisterControllers(typeof(MyMvcController).Assembly);
        } 
    }
}