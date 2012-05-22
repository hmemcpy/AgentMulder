using System.Collections.Generic;
using System.Linq;
using AgentMulder.ReSharper.Domain.Registrations;
using JetBrains.ReSharper.Psi.CSharp.Tree;
using JetBrains.ReSharper.Psi.Services.StructuralSearch;
using JetBrains.ReSharper.Psi.Tree;

namespace AgentMulder.ReSharper.Domain.Patterns
{
    public abstract class RegistrationPatternBase : IRegistrationPattern
    {
        private readonly IStructuralMatcher matcher;

        public IStructuralMatcher Matcher
        {
            get { return matcher; }
        }

        protected RegistrationPatternBase(IStructuralSearchPattern pattern)
        {
            matcher = pattern.CreateMatcher();
        }

        private IInvocationExpression GetMatchedExpression(ITreeNode rootElement)
        {
            var invocationExpression = rootElement as IInvocationExpression;
            if (invocationExpression == null)
                return null;

            return invocationExpression.GetAllExpressions().FirstOrDefault(expression =>
            {
                bool result = matcher.QuickMatch(expression);
                return result;
            });
        }

        protected IStructuralMatchResult Match(ITreeNode treeNode)
        {
            IInvocationExpression expression = GetMatchedExpression(treeNode);
            if (expression == null)
            {
                return matcher.Match(treeNode);
            }

            return matcher.Match(expression);
        }

        public abstract IEnumerable<IComponentRegistration> GetComponentRegistrations(ITreeNode registrationRootElement);
    }
}