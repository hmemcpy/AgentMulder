using System;
using System.Linq.Expressions;
using JetBrains.ReSharper.Psi.CSharp.Tree;

namespace AgentMulder.ReSharper.Domain.Expressions
{
    internal class UnaryOperatorExpressionBuilder : ExpressionBuilder<IUnaryOperatorExpression>
    {
        private readonly IUnaryOperatorExpression expression;
        private readonly Expression context;

        public UnaryOperatorExpressionBuilder(IUnaryOperatorExpression expression, Expression context)
        {
            this.expression = expression;
            this.context = context;
        }

        public override Expression Build()
        {
            switch (expression.UnaryOperatorType)
            {
                case UnaryOperatorType.EXCL:
                    return Expression.Not(context);
                default:
                    return null; // not supported yet
            }
        }
    }
}