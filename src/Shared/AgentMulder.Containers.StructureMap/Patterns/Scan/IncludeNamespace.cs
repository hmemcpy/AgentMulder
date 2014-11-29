using System.ComponentModel.Composition;
using AgentMulder.ReSharper.Domain.Patterns;
using JetBrains.ReSharper.Psi;
using JetBrains.ReSharper.Psi.CSharp.Tree;
#if SDK90
using JetBrains.ReSharper.Feature.Services.CSharp.StructuralSearch;
using JetBrains.ReSharper.Feature.Services.CSharp.StructuralSearch.Placeholders;
using JetBrains.ReSharper.Feature.Services.StructuralSearch;
#else
using JetBrains.ReSharper.Psi.Services.CSharp.StructuralSearch;
using JetBrains.ReSharper.Psi.Services.CSharp.StructuralSearch.Placeholders;
using JetBrains.ReSharper.Psi.Services.StructuralSearch;
#endif
using PsiExtensions = AgentMulder.ReSharper.Domain.Utils.PsiExtensions;

namespace AgentMulder.Containers.StructureMap.Patterns.Scan
{
    [Export(typeof(IBasedOnPattern))]
    public class IncludeNamespace : NamespaceRegistrationPatternBase
    {
        private static readonly IStructuralSearchPattern pattern =
            new CSharpStructuralSearchPattern("$scanner$.IncludeNamespace($argument$)",
                new ExpressionPlaceholder("scanner", "global::StructureMap.Graph.IAssemblyScanner", false),
                new ArgumentPlaceholder("argument", 1, 1));

        public IncludeNamespace()
            : base(pattern)
        {
        }

        protected override INamespace GetNamespaceElement(IStructuralMatchResult match, out bool includeSubnamespaces)
        {
            includeSubnamespaces = true;

            var argument = (ICSharpArgument)match.GetMatchedElement("argument");
            if (argument != null)
            {
                return PsiExtensions.GetNamespaceDeclaration(argument.Value);
            }

            return null;
        }
    }
}