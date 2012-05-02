using System;
using System.Linq;
using System.Collections.Generic;
using AgentMulder.ReSharper.Domain.Registrations;
using JetBrains.ReSharper.Psi.CSharp.Tree;
using JetBrains.ReSharper.Psi.Services.StructuralSearch;
using JetBrains.ReSharper.Psi.Tree;
using JetBrains.Util;

namespace AgentMulder.ReSharper.Domain.Search
{
    public abstract class RegistrationBasePattern : IRegistrationPattern
    {
        private readonly IStructuralSearchPattern pattern;

        protected RegistrationBasePattern(IStructuralSearchPattern pattern)
        {
            this.pattern = pattern;
            Assertion.Assert(pattern.Check() == null, "Invalid pattern");
        }

        public virtual IStructuralMatcher CreateMatcher()
        {
            return pattern.CreateMatcher();
        }

        protected IInvocationExpression GetMatchedExpression(ITreeNode rootElement)
        {
            IStructuralMatcher matcher = CreateMatcher();

            var invocationExpression = rootElement as IInvocationExpression;
            if (invocationExpression == null)
                return null;

            return invocationExpression.GetAllExpressions().FirstOrDefault(expression => matcher.QuickMatch(expression));
        }

        protected IStructuralMatchResult Match(ITreeNode treeNode)
        {
            IStructuralMatcher matcher = CreateMatcher();

            IInvocationExpression expression = GetMatchedExpression(treeNode);
            if (expression == null)
            {
                return matcher.Match(treeNode);
            }

            return matcher.Match(expression);
        }

        public abstract IEnumerable<IComponentRegistration> GetComponentRegistrations(ITreeNode parentElement);
    }
}