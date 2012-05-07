using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using JetBrains.ReSharper.Psi;
using JetBrains.ReSharper.Psi.CSharp.Tree;
using JetBrains.ReSharper.Psi.ExtensionsAPI.Resolve;
using JetBrains.ReSharper.Psi.Resolve;
using JetBrains.ReSharper.Psi.Tree;
using JetBrains.Util;

namespace AgentMulder.ReSharper.Domain
{
    public static class WherePredicateBuilder
    {
        public static Expression<Predicate<TReturn>> FromLambda<TReturn>(ILambdaExpression lambdaExpression)
        {
            var visitor = new ExpressionTreeBuilderVisitor();
            Expression body = visitor.VisitLambdaExpression(lambdaExpression, null);

            var x = Expression.Lambda<Predicate<TReturn>>(body, Expression.Parameter(typeof(TReturn)));
            return x;
        }
    }

    public class ExpressionTreeBuilderVisitor : TreeNodeVisitor<Expression, Expression>
    {
        public override Expression VisitNode(ITreeNode node, Expression context)
        {
            return base.VisitNode(node, context);
        }

        public override Expression VisitLambdaExpression(ILambdaExpression lambdaExpressionParam, Expression context)
        {
            return lambdaExpressionParam.BodyExpression.Accept(this, context);
        }

        public override Expression VisitInvocationExpression(IInvocationExpression invocationExpressionParam, Expression context)
        {
            IEnumerable<Expression> arguments = invocationExpressionParam.ArgumentList.Arguments.Select(argument => argument.Accept(this, context));

            Expression instance = invocationExpressionParam.InvokedExpression.Accept(this, context);
            if (instance == null)
            {
                return base.VisitInvocationExpression(invocationExpressionParam, context);
            }

            ResolveResultWithInfo resolveResult = invocationExpressionParam.Reference.Resolve();
            if (resolveResult.ResolveErrorType == ResolveErrorType.OK)
            {
                var method = resolveResult.DeclaredElement as IMethod;
                if (method != null)
                {
                    ITypeElement containingType = method.GetContainingType();
                    string fullName = containingType.GetClrName().FullName;
                    Type type = Type.GetType(fullName);
                    if (type != null)
                    {
                        MethodInfo methodInfo = type.GetMethod(method.ShortName);


                        return Expression.Call(instance, methodInfo, arguments);
                    }
                }
            }

            return base.VisitInvocationExpression(invocationExpressionParam, context);
        }

        public override Expression VisitCSharpArgument(ICSharpArgument cSharpArgumentParam, Expression context)
        {
            return null;

        }

        public override Expression VisitReferenceExpression(IReferenceExpression referenceExpressionParam, Expression context)
        {
            if (referenceExpressionParam.QualifierExpression != null)
            {
                return referenceExpressionParam.QualifierExpression.Accept(this, context);
            }

            return base.VisitReferenceExpression(referenceExpressionParam, context);
        }

        public override Expression VisitTypeofExpression(ITypeofExpression typeofExpressionParam, Expression context)
        {
            IDeclaredType declaredType = typeofExpressionParam.ArgumentType.GetScalarType();
            if (declaredType == null || !declaredType.IsValid())
            {
                return base.VisitTypeofExpression(typeofExpressionParam, context);
            }

            string fullyQualifiedTypeName = string.Format("{0}, {1}", declaredType.GetClrName().FullName,
                                                          declaredType.Assembly.FullName);

            Type value = Type.GetType(fullyQualifiedTypeName);
            
            return Expression.Constant(value, typeof(Type));
        }

        public override Expression VisitCSharpLiteralExpression(ICSharpLiteralExpression expression, Expression treeNode)
        {
            var declaredType = expression.ConstantValue.Type as IDeclaredType;
            if (declaredType != null)
            {
                Type type = Type.GetType(declaredType.GetClrName().FullName);
                Assertion.Assert(type != null, "type != null");
                return Expression.Constant(expression.ConstantValue.Value, type);
            }

            return Expression.Constant(expression.ConstantValue.Value);
        }
    }
}