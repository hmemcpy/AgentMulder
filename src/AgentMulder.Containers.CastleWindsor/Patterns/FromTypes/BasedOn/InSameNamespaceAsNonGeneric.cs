using System;
using System.Linq;
using AgentMulder.Containers.CastleWindsor.Patterns.FromTypes.WithService;
using AgentMulder.ReSharper.Domain;
using JetBrains.ReSharper.Psi;
using JetBrains.ReSharper.Psi.CSharp;
using JetBrains.ReSharper.Psi.CSharp.Tree;
using JetBrains.ReSharper.Psi.Services.CSharp.StructuralSearch;
using JetBrains.ReSharper.Psi.Services.CSharp.StructuralSearch.Placeholders;
using JetBrains.ReSharper.Psi.Services.StructuralSearch;
using JetBrains.ReSharper.Psi.Tree;

namespace AgentMulder.Containers.CastleWindsor.Patterns.FromTypes.BasedOn
{
    internal sealed class InSameNamespaceAsNonGeneric : NamespaceRegistrationBasePattern
    {
        private static readonly IStructuralSearchPattern pattern =
            new CSharpStructuralSearchPattern("$fromDescriptor$.InSameNamespaceAs($arguments$)",
                                              new ExpressionPlaceholder("fromDescriptor", "Castle.MicroKernel.Registration.FromDescriptor", false),
                                              new ArgumentPlaceholder("arguments", 1, 2)); // at most two occurrences, for both overloads

        public InSameNamespaceAsNonGeneric(params WithServiceRegistrationBasePattern[] withServicePatterns)
            : base(pattern, withServicePatterns)
        {
        }

        protected override INamespace GetNamespaceElement(IStructuralMatchResult match, out bool includeSubnamespaces)
        {
            var arguments = match.GetMatchedElementList("arguments").Cast<ICSharpArgument>().ToArray();

            INamespace namespaceElement = this.If(x => arguments.Length > 0)
                .Return(x => GetNamespaceDeclaration(arguments[0].Value), null);

            includeSubnamespaces = this.If(x => arguments.Length == 2)
                .With(x => arguments[arguments.Length - 1])
                .With(x => x.Value.ConstantValue)
                .If(x => x.IsBoolean())
                .Return(x => Convert.ToBoolean(x.Value), false);

            return namespaceElement;
        }

        private INamespace GetNamespaceDeclaration(ICSharpExpression expression)
        {
            return this.With(x => expression as ITypeofExpression)
                       .With(x => x.ArgumentType as IDeclaredType)
                       .With(x => x.GetTypeElement())
                       .Return(x => x.GetContainingNamespace(), null);
        }
    }
}