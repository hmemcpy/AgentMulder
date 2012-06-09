using Autofac;
using Autofac.Builder;
using Autofac.Features.Scanning;
using TestApplication.Types;

// ReSharper disable CheckNamespace <-- due to a bug in ReSharper, with namespace containing a part of the matched expression (i.e. Autofac)
namespace TestApplication.RegisterAssemblyTypesTests
// ReSharper restore CheckNamespace
{
    public class AsGeneric : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(ThisAssembly).As<ICommon>();
        }
    }
}