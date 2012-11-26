using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using AgentMulder.ReSharper.Domain.Patterns;
using AgentMulder.ReSharper.Domain.Registrations;
using JetBrains.ReSharper.Psi;
using JetBrains.ReSharper.Psi.Services.CSharp.StructuralSearch;
using JetBrains.ReSharper.Psi.Services.CSharp.StructuralSearch.Placeholders;
using JetBrains.ReSharper.Psi.Services.StructuralSearch;
using JetBrains.ReSharper.Psi.Tree;

namespace AgentMulder.Containers.StructureMap.Patterns.Scan
{
    [Export(typeof(IBasedOnPattern))]
    public class IncludeNamespaceContainingType : NamespaceRegistrationPatternBase
    {
        private static readonly IStructuralSearchPattern pattern =
            new CSharpStructuralSearchPattern("$scanner$.IncludeNamespaceContainingType<$type$>()",
                new ExpressionPlaceholder("scanner", "global::StructureMap.Graph.IAssemblyScanner"),
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