using System;
using System.Linq.Expressions;
using JetBrains.ReSharper.Psi;
using JetBrains.ReSharper.Psi.CSharp.Tree;
using JetBrains.ReSharper.Psi.Tree;
using JetBrains.Util;

namespace AgentMulder.ReSharper.Domain
{
    public static class WherePredicateBuilder
    {
        public static Predicate<TReturn> FromLambda<TReturn>(ILambdaExpression lambdaExpression)
        {
            var myVisitor = new MyVisitor();
            Expression body = myVisitor.VisitLambdaExpression(lambdaExpression, null);

            var x = Expression.Lambda<Predicate<TReturn>>(body, Expression.Parameter(typeof(TReturn)));
            return x.Compile();
        }
    }

    public class MyVisitor : TreeNodeVisitor<ITreeNode, Expression>
    {
        public override Expression VisitNode(ITreeNode node, ITreeNode context)
        {
            return base.VisitNode(node, context);
        }

        public override Expression VisitLambdaExpression(ILambdaExpression lambdaExpressionParam, ITreeNode context)
        {
            return lambdaExpressionParam.BodyExpression.Accept(this, context);
        }

        public override Expression VisitCSharpLiteralExpression(ICSharpLiteralExpression expression, ITreeNode treeNode)
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