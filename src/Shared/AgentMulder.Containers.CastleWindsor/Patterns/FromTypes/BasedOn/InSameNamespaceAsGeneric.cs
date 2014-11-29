using System;
using AgentMulder.Containers.CastleWindsor.Helpers;
using AgentMulder.Containers.CastleWindsor.Patterns.FromTypes.BasedOn.WhereArgument;
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

namespace AgentMulder.Containers.CastleWindsor.Patterns.FromTypes.BasedOn
{
    internal sealed class InSameNamespaceAsGeneric : NamespaceRegistrationPatternBase
    {
        private static readonly IStructuralSearchPattern pattern =
            new CSharpStructuralSearchPattern("$fromDescriptor$.InSameNamespaceAs<$type$>($subnamespace$)",
                new ExpressionPlaceholder("fromDescriptor", "global::Castle.MicroKernel.Registration.FromDescriptor", false),
                new TypePlaceholder("type"),
                new ArgumentPlaceholder("subnamespace", 0, 1));

        public InSameNamespaceAsGeneric()
            : base(pattern)
        {
        }

        protected override INamespace GetNamespaceElement(IStructuralMatchResult match, out bool includeSubnamespaces)
        {
            return NamespaceExtractor.GetNamespace(match, out includeSubnamespaces);
        }
    }
}