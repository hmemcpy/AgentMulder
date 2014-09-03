// Patterns: 1
// Matches: MyWebApiController.cs
// NotMatches: MyMvcController.cs

using Autofac;
using Autofac.Integration.WebApi;
using TestApplication.Types;

namespace TestApplication.Autofac.Mvc
{
    public class RegisterWebApiControllers
    {
        public RegisterWebApiControllers()
        {
            var builder = new ContainerBuilder();

            builder.RegisterApiControllers(typeof(MyMvcController).Assembly);
        }
    }
}