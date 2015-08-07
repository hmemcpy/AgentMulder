// Patterns: 1
// Matches: CommonImpl12.cs
// NotMatches: CommonImpl1.cs

using Autofac;
using TestApplication.Types;

namespace TestApplication.Autofac.RegisterAssemblyTypesTests
{
    public class AssignableToNonGeneric2 : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(ThisAssembly).AssignableTo(typeof(ICommon2)).AssignableTo(typeof(ICommon));
        }
    }
}