using System;
using System.Collections.Generic;
using JetBrains.ReSharper.Psi;
using JetBrains.ReSharper.Psi.CSharp;
using JetBrains.ReSharper.Psi.CSharp.Tree;
using JetBrains.ReSharper.Psi.Resolve;
using JetBrains.ReSharper.Psi.Tree;

namespace AgentMulder.ReSharper.Domain.Utils
{
    public static class PsiExtensions
    {
        public static IEnumerable<IInvocationExpression> GetAllExpressions(this IInvocationExpression expression)
        {
            for (var e = expression; e != null; e = ((IReferenceExpression)e.InvokedExpression).QualifierExpression as IInvocationExpression)
                yield return e;
        }

        public static bool IsGenericInterface(this ITypeElement typeElement)
        {
            return typeElement is IInterface &&
                   typeElement.HasTypeParameters();
		}

        public static bool IsGenericTypeDefinition(this ITypeElement element)
        {
            // todo check if this is enough
            return element.HasTypeParameters();
        }

        public static bool IsOpenGeneric(this IInterface @interface)
        {
            return @interface.IdSubstitution.IsId();
        }

        public static bool ClosesOver(this ITypeElement typeElement, ITypeElement openGenericType)
        {
            // todo this is probably wrong
            return typeElement.IsDescendantOf(openGenericType);
        }

        public static bool IsDelegate(this ITypeElement element)
        {
            // todo check if true;
            return element is IDelegate;
        }

        public static bool IsConcrete(this ITypeElement element)
        {
            var @class = element as IClass;
            if (@class == null)
            {
                return false;
            }

            return !@class.IsAbstract;
        }

        public static INamespace GetNamespaceDeclaration(ICSharpExpression expression)
        {
            CSharpElementFactory elementFactory = CSharpElementFactory.GetInstance(expression.GetPsiModule());

            if (expression.ConstantValue != null &&
                expression.ConstantValue.IsString())
            {
                string namespaceName = Convert.ToString(expression.ConstantValue.Value);

                return elementFactory.CreateNamespaceDeclaration(namespaceName).DeclaredElement;
            }

            return null;
        }

        public static TExpression GetParentExpression<TExpression>(this ITreeNode node)
            where TExpression : class, ITreeNode
        {
            for (var n = node; n != null; n = n.Parent)
            {
                var expressionStatement = n as TExpression;
                if (expressionStatement != null)
                    return expressionStatement;
            }

            return null;
        }

        public static IInvocationExpression GetInvocationExpression(this ITreeNode node)
        {
            // first, try to find the parent statement expression
            var statement = node.GetParentExpression<IExpressionStatement>();
            if (statement != null)
            {
                return statement.Expression as IInvocationExpression;
            }

            // otherwise, try finding the lambda expression, and take its invocation
            var lambdaExpression = node.GetParentExpression<ILambdaExpression>();
            if (lambdaExpression != null)
            {
                return lambdaExpression.BodyExpression as IInvocationExpression;
            }

            return null;
        }
    }
}