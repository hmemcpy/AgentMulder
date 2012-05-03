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

            INamespace namespaceElement = null;
            if (arguments.Length > 0)
            {
                namespaceElement = GetNamespaceDeclaration(arguments[0].Value);
            }
            
            includeSubnamespaces = false;
            if (arguments.Length == 2)
            {
                ICSharpArgument boolArgument = arguments[1];
                if (boolArgument.Value.ConstantValue != null &&
                    boolArgument.Value.ConstantValue.IsBoolean())
                {
                    includeSubnamespaces = Convert.ToBoolean(boolArgument.Value.ConstantValue.Value);
                }
            }

            return namespaceElement;
        }

        private INamespace GetNamespaceDeclaration(ICSharpExpression expression)
        {
            var typeofExpression = expression as ITypeofExpression;
            if (typeofExpression != null)
            {
                var declaredType = typeofExpression.ArgumentType as IDeclaredType;
                if (declaredType != null)
                {
                    ITypeElement typeElement = declaredType.GetTypeElement();
                    if (typeElement != null)
                    {
                        return typeElement.GetContainingNamespace();
                    }
                }
            }

            return null;
        }
    }
}