using System;
using JetBrains.ReSharper.Feature.Services.StructuralSearch;
using JetBrains.ReSharper.Psi;
using JetBrains.ReSharper.Psi.CSharp.Tree;

namespace AgentMulder.Containers.CastleWindsor.Helpers
{
    internal class InSameNamespaceAsNonGenericExtractor : INamespaceExtractor
    {
        public INamespace GetNamespace<T>(T element, out bool includeSubnamespaces)
        {
            var arguments = element as ICSharpArgument[];
            if (arguments == null)
            {
                includeSubnamespaces = false;
                return null;
            }

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

    internal class InSameNamespaceAsGenericExtractor : INamespaceExtractor
    {
        public INamespace GetNamespace<T>(T element, out bool includeSubnamespaces)
        {
            var match = element as IStructuralMatchResult;
            if (match == null)
            {
                includeSubnamespaces = false;
                return null;
            }

            includeSubnamespaces = false;
            var argument = match.GetMatchedElement("subnamespace") as ICSharpArgument;
            if (argument != null)
            {
                if (argument.Value.ConstantValue != null &&
                    argument.Value.ConstantValue.IsBoolean())
                {
                    includeSubnamespaces = Convert.ToBoolean(argument.Value.ConstantValue.Value);
                }
            }

            var declaredType = match.GetMatchedType("type") as IDeclaredType;
            if (declaredType != null)
            {
                ITypeElement typeElement = declaredType.GetTypeElement();
                if (typeElement != null)
                {
                    return typeElement.GetContainingNamespace();
                }
            }

            return null;
        }
    }
}