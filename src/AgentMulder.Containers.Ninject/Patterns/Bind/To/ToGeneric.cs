using System.ComponentModel.Composition;
using AgentMulder.ReSharper.Domain.Patterns;
#if SDK90
using JetBrains.ReSharper.Feature.Services.CSharp.StructuralSearch;
using JetBrains.ReSharper.Feature.Services.CSharp.StructuralSearch.Placeholders;
using JetBrains.ReSharper.Feature.Services.StructuralSearch;
#else
using JetBrains.ReSharper.Psi.Services.CSharp.StructuralSearch;
using JetBrains.ReSharper.Psi.Services.CSharp.StructuralSearch.Placeholders;
using JetBrains.ReSharper.Psi.Services.StructuralSearch;
#endif

namespace AgentMulder.Containers.Ninject.Patterns.Bind.To
{
    [Export(typeof(ComponentImplementationPatternBase))]
    internal sealed class ToGeneric : ComponentImplementationPatternBase
    {
        private static readonly IStructuralSearchPattern pattern = 
            new CSharpStructuralSearchPattern("$bind$.To<$type$>()",
                new ExpressionPlaceholder("bind", "global::Ninject.Syntax.IBindingSyntax", false),
                new TypePlaceholder("type"));

        public ToGeneric()
            : base(pattern, "type")
        {
        }
    }
}