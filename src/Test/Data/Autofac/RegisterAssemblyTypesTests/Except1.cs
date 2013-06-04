// Patterns: 1
// Matches: StandardCard.cs,PlatinumCard.cs
// NotMatches: GoldCard.cs

using Autofac;
using TestApplication.Types;

namespace TestApplication.Autofac.RegisterAssemblyTypesTests
{
    public class Except1 : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(ThisAssembly).Except<GoldCard>();
        }
    }
}