using System.Linq.Expressions;
using System.Reflection;
using JetBrains.ReSharper.Psi;
using JetBrains.ReSharper.Psi.CSharp.Tree;
using JetBrains.ReSharper.Psi.Resolve;
using Scully.Metadata;
using JetBrains.ReSharper.Psi.Resolve.ExtensionMethods;

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
            IResolveResult resolve = expression.Reference.Resolve().Result;
            ExtensionInstance<IDeclaredElement> element = resolve.ElementAsExtension();
            
            var property = element.Element as IProperty;
            if (property != null)
            {
                if (property.XMLDocId == "P:System.Array.Length")
                {
                    return Expression.ArrayLength(context);
                }
            }
            
            MemberInfo memberInfo = GetReferencedMember(element.Element.ShortName, context.Type);

            return Expression.MakeMemberAccess(context, memberInfo);
        }
    }
}