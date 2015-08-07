using System.ComponentModel.Composition;
using AgentMulder.ReSharper.Domain.Patterns;
using JetBrains.ReSharper.Feature.Services.CSharp.StructuralSearch;
using JetBrains.ReSharper.Feature.Services.CSharp.StructuralSearch.Placeholders;
using JetBrains.ReSharper.Feature.Services.StructuralSearch;
using JetBrains.ReSharper.Psi;

namespace AgentMulder.Containers.StructureMap.Patterns.Scan
{
    [Export(typeof(IBasedOnPattern))]
    public class IncludeNamespaceContainingType : NamespaceRegistrationPatternBase
    {
        private static readonly IStructuralSearchPattern pattern =
            new CSharpStructuralSearchPattern("$scanner$.IncludeNamespaceContainingType<$type$>()",
                new ExpressionPlaceholder("scanner", "global::StructureMap.Graph.IAssemblyScanner", false),
                new TypePlaceholder("type"));

        public IncludeNamespaceContainingType()
            : base(pattern)
        {
        }

        protected override INamespace GetNamespaceElement(IStructuralMatchResult match, out bool includeSubnamespaces)
        {
            includeSubnamespaces = true;

            var declaredType = match.GetMatchedType("type") as IDeclaredType;
            if (declaredType != null)
            {
                ITypeElement typeElement = declaredType.GetTypeElement();
                if (typeElement != null)
                {
                    return typeElement.GetContainingNamespace();
                }
            }

            return null;
        }
    }
}