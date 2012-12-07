using AgentMulder.Containers.CastleWindsor.Helpers;
using AgentMulder.ReSharper.Domain.Patterns;
using JetBrains.ReSharper.Psi;
using JetBrains.ReSharper.Psi.Services.CSharp.StructuralSearch;
using JetBrains.ReSharper.Psi.Services.CSharp.StructuralSearch.Placeholders;
using JetBrains.ReSharper.Psi.Services.StructuralSearch;

namespace AgentMulder.Containers.CastleWindsor.Patterns.FromTypes.BasedOn.WhereArgument
{
    internal class ComponentIsInSameNamespaceAsGeneric : NamespaceRegistrationPatternBase
    {
        private static readonly IStructuralSearchPattern pattern =
            new CSharpStructuralSearchPattern("$component$.IsInSameNamespaceAs<$type$>($subnamespace$)",
                new ExpressionPlaceholder("component", "Castle.MicroKernel.Registration.Component", true),
                new TypePlaceholder("type"),
                new ArgumentPlaceholder("subnamespace", 0, 1));

        public ComponentIsInSameNamespaceAsGeneric()
            : base(pattern)
        {
        }

        protected override INamespace GetNamespaceElement(IStructuralMatchResult match, out bool includeSubnamespaces)
        {
            return NamespaceExtractor.GetNamespace(match, out includeSubnamespaces);
        }
    }
}