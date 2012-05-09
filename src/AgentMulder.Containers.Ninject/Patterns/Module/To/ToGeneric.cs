using AgentMulder.ReSharper.Domain.Registrations;
using JetBrains.ReSharper.Psi.Services.CSharp.StructuralSearch;
using JetBrains.ReSharper.Psi.Services.CSharp.StructuralSearch.Placeholders;
using JetBrains.ReSharper.Psi.Services.StructuralSearch;

namespace AgentMulder.Containers.Ninject.Patterns.Module.To
{
    internal sealed class ToGeneric : ComponentImplementationBasePattern
    {
        private static readonly IStructuralSearchPattern pattern
            = new CSharpStructuralSearchPattern("$bind$.To<$service$>()",
                new ExpressionPlaceholder("bind"),
                new TypePlaceholder("service"));

        public ToGeneric()
            : base(pattern, "service")
        {
        }
    }
}