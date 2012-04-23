using JetBrains.ReSharper.Psi.Services.CSharp.StructuralSearch;
using JetBrains.ReSharper.Psi.Services.CSharp.StructuralSearch.Placeholders;
using JetBrains.ReSharper.Psi.Services.StructuralSearch;

namespace AgentMulder.Containers.CastleWindsor.Patterns.FromTypes.BasedOn
{
    internal sealed class BasedOnGeneric : BasedOnRegistrationBase
    {
        private static readonly IStructuralSearchPattern pattern = 
            new CSharpStructuralSearchPattern("$anything$.BasedOn<$type$>()",
                new ExpressionPlaceholder("anything"),
                new TypePlaceholder("type"));

        public BasedOnGeneric()
            : base(pattern, "type")
        {
        }
    }
}