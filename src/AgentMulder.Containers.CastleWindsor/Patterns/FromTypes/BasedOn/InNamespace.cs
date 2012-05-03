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

namespace AgentMulder.Containers.CastleWindsor.Patterns.FromTypes.BasedOn
{
    internal sealed class InNamespace : NamespaceRegistrationBasePattern
    {
        private static readonly IStructuralSearchPattern pattern =
            new CSharpStructuralSearchPattern("$fromDescriptor$.InNamespace($arguments$)",
                new ExpressionPlaceholder("fromDescriptor", "Castle.MicroKernel.Registration.FromDescriptor", false),
                new ArgumentPlaceholder("arguments", 1, 2)); // at most two occurrences, for both overloads

        public InNamespace(params WithServiceRegistrationBasePattern[] withServicePatterns)
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
            CSharpElementFactory elementFactory = CSharpElementFactory.GetInstance(expression.GetPsiModule());

            return this.With(x => expression.ConstantValue)
                .If(x => x.IsString())
                .With(x => Convert.ToString(x.Value))
                .With(elementFactory.CreateNamespaceDeclaration)
                .Return(x => x.DeclaredElement, null);
        }
    }
}