using System.Collections.Generic;
using System.Linq;
using AgentMulder.ReSharper.Domain.Registrations;
using JetBrains.ReSharper.Psi.Services.StructuralSearch;
using JetBrains.ReSharper.Psi.Tree;

namespace AgentMulder.ReSharper.Domain.Patterns
{
    /// <summary>
    /// A base class for a pattern that can be applied to several elements in the expression
    /// </summary>
    public abstract class MultipleMatchBasedOnPatternBase : BasedOnPatternBase
    {
        protected MultipleMatchBasedOnPatternBase(IStructuralSearchPattern pattern)
            : base(pattern)
        {
        }

        public sealed override IEnumerable<IComponentRegistration> GetComponentRegistrations(ITreeNode registrationRootElement)
        {
            return GetBasedOnRegistrations(registrationRootElement);
        }

        public override IEnumerable<BasedOnRegistrationBase> GetBasedOnRegistrations(ITreeNode registrationRootElement)
        {
            return MatchMany(registrationRootElement).Where(match => match.Matched)
                .SelectMany(match => DoCreateRegistrations(registrationRootElement, match));
        }

        protected abstract IEnumerable<BasedOnRegistrationBase> DoCreateRegistrations(ITreeNode registrationRootElement, IStructuralMatchResult match);
    }
}