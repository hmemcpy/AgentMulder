using System.Linq.Expressions;
using JetBrains.ReSharper.Psi.CSharp.Tree;

namespace AgentMulder.ReSharper.Domain.Expressions
{
    internal class ConditionalExpressionBuilder : ExpressionBuilder<IParenthesizedExpression>
    {
        private readonly IParenthesizedExpression expression;
        private readonly Expression left;
        private readonly Expression right;

        public ConditionalExpressionBuilder(IParenthesizedExpression expression, Expression left, Expression right)
        {
            this.expression = expression;
            this.left = left;
            this.right = right;
        }

        public override Expression Build()
        {
            // todo bleh
            var conditionalAndExpression = expression.Expression as IConditionalAndExpression;
            if (conditionalAndExpression != null)
            {
                return Expression.AndAlso(left, right);
            }

            return null;
        }
    }
}