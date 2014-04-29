using System;
using AgentMulder.Containers.CastleWindsor.Helpers;
using AgentMulder.Containers.CastleWindsor.Patterns.FromTypes.BasedOn.WhereArgument;
using AgentMulder.ReSharper.Domain.Patterns;
using JetBrains.ReSharper.Psi;
using JetBrains.ReSharper.Psi.CSharp.Tree;
using JetBrains.ReSharper.Psi.Services.CSharp.StructuralSearch;
using JetBrains.ReSharper.Psi.Services.CSharp.StructuralSearch.Placeholders;
using JetBrains.ReSharper.Psi.Services.StructuralSearch;

namespace AgentMulder.Containers.CastleWindsor.Patterns.FromTypes.BasedOn
{
    internal sealed class InSameNamespaceAsGeneric : NamespaceRegistrationPatternBase
    {
        private static readonly IStructuralSearchPattern pattern =
            new CSharpStructuralSearchPattern("$fromDescriptor$.InSameNamespaceAs<$type$>($subnamespace$)",
            // ReSharper disable RedundantArgumentDefaultValue
            // Note: in R# 6.1, the value 'false' is not the default! Don't remove this, otherwise 6.1 matching will fail!
                new ExpressionPlaceholder("fromDescriptor", "global::Castle.MicroKernel.Registration.FromDescriptor", false),
            // ReSharper restore RedundantArgumentDefaultValue

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