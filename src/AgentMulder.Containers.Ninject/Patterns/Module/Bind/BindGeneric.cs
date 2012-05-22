using AgentMulder.ReSharper.Domain.Patterns;
using JetBrains.ReSharper.Psi.Services.CSharp.StructuralSearch;
using JetBrains.ReSharper.Psi.Services.CSharp.StructuralSearch.Placeholders;
using JetBrains.ReSharper.Psi.Services.StructuralSearch;

namespace AgentMulder.Containers.Ninject.Patterns.Module.Bind
{
    internal sealed class BindGeneric : BindBasePattern
    {
        private static readonly IStructuralSearchPattern pattern
            = new CSharpStructuralSearchPattern("Bind<$service$>()",
                new TypePlaceholder("service"));

        public BindGeneric(params ComponentImplementationPatternBase[] toPatterns)
            : base(pattern, "service", toPatterns)
        {
        }
    }
}