using AgentMulder.ReSharper.Domain.Registrations;
using JetBrains.ReSharper.Psi.Services.CSharp.StructuralSearch;
using JetBrains.ReSharper.Psi.Services.CSharp.StructuralSearch.Placeholders;
using JetBrains.ReSharper.Psi.Services.StructuralSearch;

namespace AgentMulder.Containers.CastleWindsor.Patterns.Component.ImplementedBy
{
    internal class ImplementedByGeneric : ComponentImplementationBasePattern
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