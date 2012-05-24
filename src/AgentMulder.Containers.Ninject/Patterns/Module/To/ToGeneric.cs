using AgentMulder.ReSharper.Domain.Patterns;
using JetBrains.ReSharper.Psi.Services.CSharp.StructuralSearch;
using JetBrains.ReSharper.Psi.Services.CSharp.StructuralSearch.Placeholders;
using JetBrains.ReSharper.Psi.Services.StructuralSearch;

namespace AgentMulder.Containers.Ninject.Patterns.Module.To
{
    internal sealed class ToGeneric : ComponentImplementationPatternBase
    {
        public ToGeneric(bool useQualifier)
            : base(CreatePattern(useQualifier), "type")
        {
        }

        private static IStructuralSearchPattern CreatePattern(bool useQualifier)
        {
            // note read the wiki page on Ninject to understand why I had to do this

            if (useQualifier)
            {
                return new CSharpStructuralSearchPattern("$bind$.To<$type$>()",
                    new ExpressionPlaceholder("bind", "global::Ninject.Syntax.IBindingSyntax", false),
                    new TypePlaceholder("type"));
            }

            return new CSharpStructuralSearchPattern("To<$type$>()", new TypePlaceholder("type"));
        }
    }
}