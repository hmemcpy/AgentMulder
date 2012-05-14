using System.Linq.Expressions;
using System.Reflection;
using JetBrains.ReSharper.Psi.CSharp.Tree;
using Scully.Metadata;

namespace AgentMulder.ReSharper.Domain.Expressions
{
    internal class MemberReferenceExpressionBuilder : ExpressionBuilder<IReferenceExpression>
    {
        private readonly IReferenceExpression expression;
        private readonly Expression context;
        private readonly IMetadataResolver metadataResolver;

        public MemberReferenceExpressionBuilder(IReferenceExpression expression, Expression context, IMetadataResolver metadataResolver)
        {
            this.expression = expression;
            this.context = context;
            this.metadataResolver = metadataResolver;
        }

        public override Expression Build()
        {
            MemberInfo memberInfo = GetReferencedMember(expression.NameIdentifier.GetText(), context.Type);

            return Expression.MakeMemberAccess(context, memberInfo);
        }
    }
}