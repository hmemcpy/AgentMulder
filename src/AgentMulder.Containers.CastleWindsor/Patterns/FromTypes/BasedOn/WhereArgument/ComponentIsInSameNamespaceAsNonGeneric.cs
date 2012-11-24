using System.Linq;
using AgentMulder.Containers.CastleWindsor.Helpers;
using AgentMulder.ReSharper.Domain.Patterns;
using JetBrains.ReSharper.Psi;
using JetBrains.ReSharper.Psi.CSharp.Tree;
using JetBrains.ReSharper.Psi.Services.CSharp.StructuralSearch;
using JetBrains.ReSharper.Psi.Services.CSharp.StructuralSearch.Placeholders;
using JetBrains.ReSharper.Psi.Services.StructuralSearch;

namespace AgentMulder.Containers.CastleWindsor.Patterns.FromTypes.BasedOn.WhereArgument
{
    internal class ComponentIsInSameNamespaceAsNonGeneric : NamespaceRegistrationPatternBase
    {
        private static readonly IStructuralSearchPattern pattern =
            new CSharpStructuralSearchPattern("$component$.IsInSameNamespaceAs($arguments$)",
                new ExpressionPlaceholder("component", "Castle.MicroKernel.Registration.Component", true),
                new ArgumentPlaceholder("arguments", 1, 2));

        public ComponentIsInSameNamespaceAsNonGeneric()
            : base(pattern)
        {
        }

        protected override INamespace GetNamespaceElement(IStructuralMatchResult match, out bool includeSubnamespaces)
        {
            var arguments = match.GetMatchedElementList("arguments").Cast<ICSharpArgument>().ToArray();

            return NamespaceExtractor.GetNamespace(arguments, out includeSubnamespaces);
        }
    }
}