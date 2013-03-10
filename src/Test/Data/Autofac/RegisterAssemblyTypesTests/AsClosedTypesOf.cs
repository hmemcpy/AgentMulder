// Patterns: 1
// Matches: SaveCommand.cs
// NotMatches: Foo.cs

using Autofac;
using Autofac.Core;
using TestApplication.Types;

namespace TestApplication.Autofac.RegisterAssemblyTypesTests
{
    public class AsClosedTypesOf : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(ThisAssembly)
            .AsClosedTypesOf(typeof(ICommand<>))
            .AsImplementedInterfaces()
            // rest of the methods are from a bug report, they are not relevant
            .PropertiesAutowired(PropertyWiringOptions.PreserveSetValues)
            .InstancePerLifetimeScope()
            .OnActivated(OnActivatedService);
        }

        private void OnActivatedService(IActivatedEventArgs<object> obj)
        {
            
        }
    }
}