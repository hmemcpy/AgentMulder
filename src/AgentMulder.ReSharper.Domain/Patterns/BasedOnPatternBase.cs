using System.Collections.Generic;
using System.ComponentModel.Composition;
using AgentMulder.ReSharper.Domain.Registrations;
using JetBrains.ReSharper.Psi.Services.StructuralSearch;
using JetBrains.ReSharper.Psi.Tree;

namespace AgentMulder.ReSharper.Domain.Patterns
{
    [InheritedExport(typeof(IBasedOnPattern))]
    public abstract class BasedOnPatternBase : RegistrationPatternBase, IBasedOnPattern
    {
        protected BasedOnPatternBase(IStructuralSearchPattern pattern)
            :base(pattern)
        {
        }

        IEnumerable<IComponentRegistration> IRegistrationPattern.GetComponentRegistrations(ITreeNode registrationRootElement)
        {
            return GetBasedOnRegistrations(registrationRootElement);
        }

        public abstract IEnumerable<BasedOnRegistrationBase> GetBasedOnRegistrations(ITreeNode registrationRootElement);
    }
}