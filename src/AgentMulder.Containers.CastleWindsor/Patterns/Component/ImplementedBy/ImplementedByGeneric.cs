using AgentMulder.ReSharper.Domain.Patterns;
using AgentMulder.ReSharper.Domain.Registrations;
#if SDK90
using JetBrains.ReSharper.Feature.Services.CSharp.StructuralSearch;
using JetBrains.ReSharper.Feature.Services.CSharp.StructuralSearch.Placeholders;
using JetBrains.ReSharper.Feature.Services.StructuralSearch;
#else
using JetBrains.ReSharper.Psi.Services.CSharp.StructuralSearch;
using JetBrains.ReSharper.Psi.Services.CSharp.StructuralSearch.Placeholders;
using JetBrains.ReSharper.Psi.Services.StructuralSearch;
#endif

namespace AgentMulder.Containers.CastleWindsor.Patterns.Component.ImplementedBy
{
    internal class ImplementedByGeneric : ComponentImplementationPatternBase
    {
        private static readonly IStructuralSearchPattern pattern =
            new CSharpStructuralSearchPattern("$anything$.ImplementedBy<$impl$>()",
                                              new ExpressionPlaceholder("anything"),
                                              new TypePlaceholder("impl"));

        public ImplementedByGeneric()
            : base(pattern, "impl")
        {
        }
    }
}