using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using JetBrains.ReSharper.Psi;
using JetBrains.ReSharper.Psi.CSharp.Tree;
using Scully.Metadata;

namespace AgentMulder.ReSharper.Domain.Expressions
{
    internal class MethodCallExpressionBuilder : ExpressionBuilder<IReferenceExpression>
    {
        private readonly IReferenceExpression reference;
        private readonly Expression context;
        private readonly IEnumerable<Expression> arguments;
        private readonly IMetadataResolver metadataResolver;

        public MethodCallExpressionBuilder(IReferenceExpression reference, Expression context, IEnumerable<Expression> arguments, IMetadataResolver metadataResolver)
        {
            this.reference = reference;
            this.context = context;
            this.arguments = arguments;
            this.metadataResolver = metadataResolver;
        }

        public override Expression Build()
        {
            MemberInfo memberInfo = GetReferencedMember(reference.NameIdentifier.GetText(), context.Type);

            return Expression.Call(context, (MethodInfo)memberInfo, arguments);
        }
    }
}