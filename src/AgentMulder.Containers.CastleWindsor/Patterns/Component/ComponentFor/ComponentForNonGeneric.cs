using AgentMulder.Containers.CastleWindsor.Patterns.Component.ImplementedBy;
using JetBrains.ReSharper.Psi.Services.CSharp.StructuralSearch;
using JetBrains.ReSharper.Psi.Services.CSharp.StructuralSearch.Placeholders;
using JetBrains.ReSharper.Psi.Services.StructuralSearch;

namespace AgentMulder.Containers.CastleWindsor.Patterns.Component.ComponentFor
{
    internal sealed class ComponentForNonGeneric : ComponentForBase
    {
        private static readonly IStructuralSearchPattern pattern =
            new CSharpStructuralSearchPattern("$component$.For(typeof($service$))",
                                              new ExpressionPlaceholder("component", "Castle.MicroKernel.Registration.Component"),
                                              new TypePlaceholder("service"));

        public ComponentForNonGeneric(params ImplementedByBase[] implementedByPatterns)
            : base(pattern, "service", implementedByPatterns)
        {

        }
    }
}