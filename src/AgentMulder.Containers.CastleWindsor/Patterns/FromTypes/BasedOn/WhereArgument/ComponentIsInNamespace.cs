using System.Linq;
using AgentMulder.Containers.CastleWindsor.Helpers;
using AgentMulder.ReSharper.Domain.Patterns;
using JetBrains.ReSharper.Feature.Services.CSharp.StructuralSearch;
using JetBrains.ReSharper.Feature.Services.CSharp.StructuralSearch.Placeholders;
using JetBrains.ReSharper.Feature.Services.StructuralSearch;
using JetBrains.ReSharper.Psi;
using JetBrains.ReSharper.Psi.CSharp.Tree;

namespace AgentMulder.Containers.CastleWindsor.Patterns.FromTypes.BasedOn.WhereArgument
{
    internal class ComponentIsInNamespace : NamespaceRegistrationPatternBase
    {
        private static readonly IStructuralSearchPattern pattern = 
            new CSharpStructuralSearchPattern("$component$.IsInNamespace($arguments$)",
                new ExpressionPlaceholder("component", "Castle.MicroKernel.Registration.Component", true),
                new ArgumentPlaceholder("arguments", 1, 2));

        public ComponentIsInNamespace()
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