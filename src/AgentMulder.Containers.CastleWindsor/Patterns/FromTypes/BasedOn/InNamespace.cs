using System.Collections.Generic;
using System.Linq;
using AgentMulder.Containers.CastleWindsor.Patterns.FromTypes.WithService;
using AgentMulder.ReSharper.Domain;
using AgentMulder.ReSharper.Domain.Registrations;
using JetBrains.ReSharper.Psi;
using JetBrains.ReSharper.Psi.CSharp;
using JetBrains.ReSharper.Psi.CSharp.Tree;
using JetBrains.ReSharper.Psi.Services.CSharp.StructuralSearch;
using JetBrains.ReSharper.Psi.Services.CSharp.StructuralSearch.Placeholders;
using JetBrains.ReSharper.Psi.Services.StructuralSearch;
using JetBrains.ReSharper.Psi.Tree;

namespace AgentMulder.Containers.CastleWindsor.Patterns.FromTypes.BasedOn
{
    internal sealed class InNamespace : BasedOnRegistrationBasePattern
    {
        private static readonly IStructuralSearchPattern pattern =
            new CSharpStructuralSearchPattern("$fromDescriptor$.InNamespace($arguments$)",
                new ExpressionPlaceholder("fromDescriptor", "Castle.MicroKernel.Registration.FromDescriptor", false),
                new ArgumentPlaceholder("arguments", 1, 2)); // at most two occurrences, for both overloads

        public InNamespace(params WithServiceRegistrationBasePattern[] withServicePatterns)
            : base(pattern, withServicePatterns)
        {
        }

        public override IEnumerable<IComponentRegistration> GetComponentRegistrations(ITreeNode parentElement)
        {
            var match = Match(parentElement);

            if (match.Matched)
            {
                var arguments = match.GetMatchedElementList("arguments").Cast<ICSharpArgument>().ToArray();
                
                bool includeSubnamespaces = false;
                INamespace namespaceElement;
                switch (arguments.Length)
                {
                    case 1:
                        namespaceElement = GetNamespaceDeclaration(arguments[0].Value);
                        break;
                    case 2:
                        namespaceElement = GetNamespaceDeclaration(arguments[0].Value);
                        includeSubnamespaces = (bool)arguments[1].Value.ConstantValue.Value;
                        break;
                    default:
                        yield break;
                }

                var withServiceRegistrations = base.GetComponentRegistrations(parentElement).OfType<WithServiceRegistration>();
                
                yield return new InNamespaceRegistration(parentElement.GetDocumentRange(), namespaceElement, includeSubnamespaces, withServiceRegistrations);
            }
        }

        private INamespace GetNamespaceDeclaration(ICSharpExpression expression)
        {
            CSharpElementFactory elementFactory = CSharpElementFactory.GetInstance(expression.GetPsiModule());

            var namespaceDeclaration = elementFactory.CreateNamespaceDeclaration((string)expression.ConstantValue.Value);

            return namespaceDeclaration.DeclaredElement;
        }
    }
}