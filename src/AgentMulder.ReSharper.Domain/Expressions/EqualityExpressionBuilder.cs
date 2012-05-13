using System;
using System.Linq.Expressions;
using JetBrains.ReSharper.Psi.CSharp.Tree;

namespace AgentMulder.ReSharper.Domain.Expressions
{
    internal class EqualityExpressionBuilder : ExpressionBuilder<IEqualityExpression>
    {
        private readonly IEqualityExpression expression;
        private readonly Expression left;
        private readonly Expression right;

        public EqualityExpressionBuilder(IEqualityExpression expression, Expression left, Expression right)
        {
            this.expression = expression;
            this.left = left;
            this.right = right;
        }

        public override Expression Build()
        {
            switch (expression.EqualityType)
            {
                case EqualityExpressionType.EQEQ:
                    return Expression.Equal(left, right);
                case EqualityExpressionType.NE:
                    return Expression.NotEqual(left, right);
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}