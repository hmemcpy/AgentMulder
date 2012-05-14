using System.Linq.Expressions;
using JetBrains.ReSharper.Psi.CSharp.Parsing;
using JetBrains.ReSharper.Psi.CSharp.Tree;
using JetBrains.ReSharper.Psi.Parsing;

namespace AgentMulder.ReSharper.Domain.Expressions
{
    internal class BinaryExpressionBuilder : ExpressionBuilder<IBinaryExpression>
    {
        private readonly IBinaryExpression expression;
        private readonly Expression left;
        private readonly Expression right;

        public BinaryExpressionBuilder(IBinaryExpression expression, Expression left, Expression right)
        {
            this.expression = expression;
            this.left = left;
            this.right = right;
        }

        public override Expression Build()
        {
            if (expression is IConditionalAndExpression)
                return Expression.AndAlso(left, right);
            if (expression is IConditionalOrExpression)
                return Expression.OrElse(left, right);

            var relationalExpression = expression as IRelationalExpression;
            if (relationalExpression != null)
            {
                TokenNodeType tokenType = relationalExpression.OperatorSign.GetTokenType();
                if (tokenType == CSharpTokenType.GT)
                    return Expression.GreaterThan(left, right);
                if (tokenType == CSharpTokenType.GE)
                    return Expression.GreaterThanOrEqual(left, right);
            }

            return null;
        }
    }
}