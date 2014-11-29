using System;
using System.Linq;
using AgentMulder.Containers.CastleWindsor.Helpers;
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
    internal sealed class InSameNamespaceAsNonGeneric : NamespaceRegistrationPatternBase
    {
        private static readonly IStructuralSearchPattern pattern =
            new CSharpStructuralSearchPattern("$fromDescriptor$.InSameNamespaceAs($arguments$)",
                new ExpressionPlaceholder("fromDescriptor", "global::Castle.MicroKernel.Registration.FromDescriptor", false),
                new ArgumentPlaceholder("arguments", 1, 2)); // at most two occurrences, for both overloads

        public InSameNamespaceAsNonGeneric()
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