using System.Collections.Generic;
using System.Linq;
using JetBrains.ReSharper.Psi;
using JetBrains.ReSharper.Psi.CSharp.Tree;
using JetBrains.ReSharper.Psi.Tree;

namespace AgentMulder.ReSharper.Domain.Utils
{
    public static class ContainerExtensions
    {
        public static bool IsContainerCall(this ITreeNode node, string containerClrTypeName)
        {
            var invocationExpression = node as IInvocationExpression;
            if (invocationExpression == null)
            {
                return false;
            }

            var resolve = invocationExpression.InvocationExpressionReference.Resolve().Result;
            var method = resolve.DeclaredElement as IMethod;
            if (method == null)
            {
                return false;
            }
            ITypeElement containingType = method.GetContainingType();
            if (containingType == null)
            {
                return false;
            }

            IDeclaredType containerClrType = CreateTypeByClrName(node, containerClrTypeName);

            return containingType.IsDescendantOf(containerClrType.GetTypeElement());
        }

        public static IDeclaredType CreateTypeByClrName(ITreeNode node, string containerClrTypeName)
        {
            return TypeFactory.CreateTypeByCLRName(containerClrTypeName, node.GetPsiModule(), node.GetResolveContext());
        }

        public static IEnumerable<ITypeElement> GetRegisteredTypes(this ICSharpExpression expression)
        {
            // match typeof() expressions
            var typeOfExpression = expression as ITypeofExpression;
            if (typeOfExpression != null)
            {
                var typeElement = (IDeclaredType)typeOfExpression.ArgumentType;

                yield return typeElement.GetTypeElement();
            }

            // match new[] or new Type[] expressions
            var arrayExpression = expression as IArrayCreationExpression;
            if (arrayExpression != null)
            {
                foreach (var initializer in arrayExpression.ArrayInitializer.ElementInitializers.OfType<IExpressionInitializer>())
                {
                    foreach (ITypeElement type in initializer.Value.GetRegisteredTypes())
                    {
                        yield return type;
                    }
                }
            }

            // match new List<Type> expressions
            var objectCreationExpression = expression as IObjectCreationExpression;
            if (objectCreationExpression != null)
            {
                foreach (var initializer in objectCreationExpression.Initializer.InitializerElements.OfType<ICollectionElementInitializer>())
                {
                    // todo find out if THERE CAN BE ONLY ONE!!!1
                    if (initializer.Arguments.Count != 1)
                    {
                        continue;
                    }

                    foreach (ITypeElement type in initializer.Arguments[0].Value.GetRegisteredTypes())
                    {
                        yield return type;
                    }
                }
            }
        }
    }
}