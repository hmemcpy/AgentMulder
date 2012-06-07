using System.Collections.Generic;
using AgentMulder.ReSharper.Domain.Registrations;
using JetBrains.ReSharper.Psi.Services.StructuralSearch;
using JetBrains.ReSharper.Psi.Tree;

namespace AgentMulder.ReSharper.Domain.Patterns
{
    public abstract class BasedOnPatternBase : RegistrationPatternBase, IBasedOnPattern
    {
        protected readonly IBasedOnRegistrationCreator registrationCreator;

        protected BasedOnPatternBase(IStructuralSearchPattern pattern, IBasedOnRegistrationCreator registrationCreator)
            :base(pattern)
        {
            this.registrationCreator = registrationCreator;
        }

        IEnumerable<IComponentRegistration> IRegistrationPattern.GetComponentRegistrations(ITreeNode registrationRootElement)
        {
            return GetBasedOnRegistrations(registrationRootElement);
        }

        public abstract IEnumerable<BasedOnRegistrationBase> GetBasedOnRegistrations(ITreeNode registrationRootElement);
    }
}