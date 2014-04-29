using System;
using System.Linq;
using AgentMulder.Containers.CastleWindsor.Helpers;
using AgentMulder.ReSharper.Domain.Patterns;
using JetBrains.ReSharper.Psi;
using JetBrains.ReSharper.Psi.CSharp.Tree;
using JetBrains.ReSharper.Psi.Services.CSharp.StructuralSearch;
using JetBrains.ReSharper.Psi.Services.CSharp.StructuralSearch.Placeholders;
using JetBrains.ReSharper.Psi.Services.StructuralSearch;

namespace AgentMulder.Containers.CastleWindsor.Patterns.FromTypes.BasedOn
{
    internal sealed class InSameNamespaceAsNonGeneric : NamespaceRegistrationPatternBase
    {
        private static readonly IStructuralSearchPattern pattern =
            new CSharpStructuralSearchPattern("$fromDescriptor$.InSameNamespaceAs($arguments$)",
                // ReSharper disable RedundantArgumentDefaultValue
                // Note: in R# 6.1, the value 'false' is not the default! Don't remove this, otherwise 6.1 matching will fail!
                new ExpressionPlaceholder("fromDescriptor", "global::Castle.MicroKernel.Registration.FromDescriptor", false),
                // ReSharper restore RedundantArgumentDefaultValue
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