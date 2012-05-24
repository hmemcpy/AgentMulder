using AgentMulder.ReSharper.Domain.Patterns;
using JetBrains.ReSharper.Psi.Services.CSharp.StructuralSearch;
using JetBrains.ReSharper.Psi.Services.CSharp.StructuralSearch.Placeholders;
using JetBrains.ReSharper.Psi.Services.StructuralSearch;

namespace AgentMulder.Containers.Ninject.Patterns.Module.Bind
{
    internal sealed class BindGeneric : BindBasePattern
    {
        public BindGeneric(bool useQualifier, params ComponentImplementationPatternBase[] toPatterns)
            : base(CreatePattern(useQualifier), "service", toPatterns)
        {
        }

        private static IStructuralSearchPattern CreatePattern(bool useQualifier)
        {
            // note read the wiki page on Ninject to understand why I had to do this
            
            if (useQualifier)
            {
                return new CSharpStructuralSearchPattern("$kernel$.Bind<$service$>()",
                    new ExpressionPlaceholder("kernel", "global::Ninject.Syntax.BindingRoot", false),
                    new TypePlaceholder("service"));
            }

            return new CSharpStructuralSearchPattern("Bind<$service$>()", new TypePlaceholder("service"));
        }
    }
}