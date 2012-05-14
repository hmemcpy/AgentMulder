using System;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using JetBrains.ReSharper.Psi.Tree;

namespace AgentMulder.ReSharper.Domain.Expressions
{
    public abstract class ExpressionBuilder<T> : IExpressionBuilder where T : ITreeNode
    {
        public abstract Expression Build();

        protected MemberInfo GetReferencedMember(string name, Type contextType)
        {
            return contextType.GetMembers().FirstOrDefault(
                info => info.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
        }
    }
}