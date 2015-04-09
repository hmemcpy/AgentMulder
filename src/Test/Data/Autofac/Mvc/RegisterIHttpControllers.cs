// Patterns: 1
// Matches: MyIHttpController.cs, MyWebApiController.cs
// NotMatches: MyMvcController.cs

using Autofac;
using Autofac.Integration.WebApi;
using TestApplication.Types;

namespace TestApplication.Autofac.Mvc
{
    public class RegisterIHttpControllers
    {
        public RegisterIHttpControllers()
        {
            var builder = new ContainerBuilder();

            builder.RegisterApiControllers(typeof(MyIHttpController).Assembly);
        }
    }
}