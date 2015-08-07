using System.ComponentModel.Composition;
using AgentMulder.ReSharper.Domain.Patterns;
using JetBrains.ReSharper.Feature.Services.CSharp.StructuralSearch;
using JetBrains.ReSharper.Feature.Services.CSharp.StructuralSearch.Placeholders;
using JetBrains.ReSharper.Feature.Services.StructuralSearch;

namespace AgentMulder.Containers.Ninject.Patterns.Bind
{
    internal sealed class ModuleRebindGeneric : BindBasePattern
    {
        private static readonly IStructuralSearchPattern pattern =
            new CSharpStructuralSearchPattern("Rebind<$service$>()",
                new TypePlaceholder("service"));

        [ImportingConstructor]
        public ModuleRebindGeneric([ImportMany] params ComponentImplementationPatternBase[] toPatterns)
            : base(pattern, "service", toPatterns)
        {
        }
    }
}