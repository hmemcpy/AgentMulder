using System;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using JetBrains.ReSharper.Psi.CSharp.Tree;

namespace AgentMulder.ReSharper.Domain.Expressions
{
    internal class MemberExpressionBuilder : ExpressionBuilder<IReferenceExpression>
    {
        private readonly IReferenceExpression expression;
        private readonly Expression context;
        private readonly IMetadataResolver metadataResolver;

        public MemberExpressionBuilder(IReferenceExpression expression, Expression context, IMetadataResolver metadataResolver)
        {
            this.expression = expression;
            this.context = context;
            this.metadataResolver = metadataResolver;
        }

        public override Expression Build()
        {
            MemberInfo memberInfo = GetReferencedMethod(expression.NameIdentifier.GetText(), context.Type);
            switch (memberInfo.MemberType)
            {
                case MemberTypes.Method:
                    return Expression.Call(context, (MethodInfo)memberInfo);
                case MemberTypes.Property:
                    return Expression.MakeMemberAccess(context, memberInfo);
                case MemberTypes.Event:
                case MemberTypes.Field:
                    throw new NotImplementedException();
            }

            return Expression.MakeMemberAccess(context, memberInfo);
        }

        private MemberInfo GetReferencedMethod(string name, Type contextType)
        {
            return contextType.GetMembers().FirstOrDefault(
                    info => info.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
        }
    }
}