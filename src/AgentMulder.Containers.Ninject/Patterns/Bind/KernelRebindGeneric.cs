using System.ComponentModel.Composition;
using AgentMulder.ReSharper.Domain.Patterns;
using JetBrains.ReSharper.Feature.Services.CSharp.StructuralSearch;
using JetBrains.ReSharper.Feature.Services.CSharp.StructuralSearch.Placeholders;
using JetBrains.ReSharper.Feature.Services.StructuralSearch;

namespace AgentMulder.Containers.Ninject.Patterns.Bind
{
    internal sealed class KernelRebindGeneric : BindBasePattern
    {
        private static readonly IStructuralSearchPattern pattern =
            new CSharpStructuralSearchPattern("$kernel$.Rebind<$service$>()",
                new ExpressionPlaceholder("kernel", "global::Ninject.Syntax.IBindingRoot", false),
                new TypePlaceholder("service"));

        [ImportingConstructor]
        public KernelRebindGeneric([ImportMany] params ComponentImplementationPatternBase[] toPatterns)
            : base(pattern, "service", toPatterns)
        {
        }
    }
}