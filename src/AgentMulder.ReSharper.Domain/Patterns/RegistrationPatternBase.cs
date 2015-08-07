using System.Collections.Generic;
using System.Linq;
using AgentMulder.ReSharper.Domain.Registrations;
using AgentMulder.ReSharper.Domain.Utils;
using JetBrains.ReSharper.Psi;
using JetBrains.ReSharper.Psi.CSharp.Tree;
#if SDK90
using JetBrains.ReSharper.Feature.Services.StructuralSearch;
#else
using JetBrains.ReSharper.Psi.Services.StructuralSearch;
#endif
using JetBrains.ReSharper.Psi.Tree;
using JetBrains.Util;

namespace AgentMulder.ReSharper.Domain.Patterns
{
    public abstract class RegistrationPatternBase : IRegistrationPattern
    {
        private readonly IStructuralSearchPattern pattern;
        private readonly IStructuralMatcher matcher;

        IStructuralSearchPattern IStructuralPatternHolder.Pattern
        {
            get { return pattern; }
        }

        IStructuralMatcher IStructuralPatternHolder.Matcher
        {
            get { return matcher; }
        }

        public PsiLanguageType Language
        {
            get { return pattern.Language; }
        }

        protected RegistrationPatternBase(IStructuralSearchPattern pattern)
        {
            this.pattern = pattern;

            matcher = pattern.CreateMatcher();
        }

        private IInvocationExpression GetMatchedExpression(ITreeNode element)
        {
            return GetAllMatchedExpressions(element).FirstOrDefault();
        }

        private IEnumerable<IInvocationExpression> GetAllMatchedExpressions(ITreeNode element)
        {
            var invocationExpression = element as IInvocationExpression;
            if (invocationExpression == null)
            {
                return EmptyList<IInvocationExpression>.InstanceList;
            }

            return invocationExpression.GetAllExpressions().Where(expression => matcher.QuickMatch(expression));
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

        protected IEnumerable<IStructuralMatchResult> MatchMany(ITreeNode treeNode)
        {
            return GetAllMatchedExpressions(treeNode).Select(expression => matcher.Match(expression));
        }

        public abstract IEnumerable<IComponentRegistration> GetComponentRegistrations(ITreeNode registrationRootElement);
    }
}