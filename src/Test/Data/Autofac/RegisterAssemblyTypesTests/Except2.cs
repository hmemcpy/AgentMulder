// Patterns: 1
// Matches: PlatinumCard.cs
// NotMatches: StandardCard.cs,GoldCard.cs

using Autofac;
using TestApplication.Types;

namespace TestApplication.Autofac.RegisterAssemblyTypesTests
{
    public class Except2 : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(ThisAssembly).Except<GoldCard>().Except<StandardCard>();
        }
    }
}