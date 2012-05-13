using System;
using System.Linq.Expressions;
using JetBrains.ReSharper.Psi.Tree;

namespace AgentMulder.ReSharper.Domain.Expressions
{
    public abstract class ExpressionBuilder<T> : IExpressionBuilder where T : ITreeNode
    {
        public abstract Expression Build();
    }
}