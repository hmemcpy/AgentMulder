using System;
using System.Linq;
using AgentMulder.Containers.CastleWindsor.Patterns.FromTypes.WithService;
using AgentMulder.ReSharper.Domain;
using AgentMulder.ReSharper.Domain.Registrations;
using JetBrains.ReSharper.Psi;
using JetBrains.ReSharper.Psi.CSharp.Tree;
using JetBrains.ReSharper.Psi.Services.CSharp.StructuralSearch;
using JetBrains.ReSharper.Psi.Services.CSharp.StructuralSearch.Placeholders;
using JetBrains.ReSharper.Psi.Services.StructuralSearch;

namespace AgentMulder.Containers.CastleWindsor.Patterns.FromTypes.BasedOn
{
    internal sealed class InSameNamespaceAsGeneric : NamespaceRegistrationBasePattern
    {
        private static readonly IStructuralSearchPattern pattern =
            new CSharpStructuralSearchPattern("$fromDescriptor$.InSameNamespaceAs<$type$>($subnamespace$)",
                new ExpressionPlaceholder("fromDescriptor", "Castle.MicroKernel.Registration.FromDescriptor", false),
                new TypePlaceholder("type"),
                new ArgumentPlaceholder("subnamespace", 0, 1));

        public InSameNamespaceAsGeneric(params WithServiceRegistrationBasePattern[] withServicePatterns)
            : base(pattern, withServicePatterns)
        {
        }

        protected override INamespace GetNamespaceElement(IStructuralMatchResult match, out bool includeSubnamespaces)
        {
            includeSubnamespaces = this.With(x => match.GetMatchedElement("subnamespace") as ICSharpArgument)
                                       .With(x => x.Value.ConstantValue)
                                       .If(x => x.IsBoolean())
                                       .Return(x => Convert.ToBoolean(x.Value), false);


            return this.With(x => match.GetMatchedType("type") as IDeclaredType)
                       .With(x => x.GetTypeElement())
                       .Return(x => x.GetContainingNamespace(), null);
        }
    }
}