using System.Collections.Generic;
using AgentMulder.ReSharper.Domain.Registrations;
using JetBrains.ReSharper.Feature.Services.StructuralSearch;
using JetBrains.ReSharper.Psi;
using JetBrains.ReSharper.Psi.Tree;

namespace AgentMulder.ReSharper.Domain.Patterns
{
    public abstract class NamespaceRegistrationPatternBase : BasedOnPatternBase
    {
        protected NamespaceRegistrationPatternBase(IStructuralSearchPattern pattern)
            : base(pattern)
        {
        }

        public override IEnumerable<IComponentRegistration> GetComponentRegistrations(ITreeNode registrationRootElement)
        {
            return GetBasedOnRegistrations(registrationRootElement);
        }

        public override IEnumerable<FilteredRegistrationBase> GetBasedOnRegistrations(ITreeNode registrationRootElement)
        {
            IStructuralMatchResult match = Match(registrationRootElement);

            if (match.Matched)
            {
                bool includeSubnamespaces;
                INamespace namespaceElement = GetNamespaceElement(match, out includeSubnamespaces);
                if (namespaceElement != null)
                {
                    yield return new InNamespaceRegistration(registrationRootElement, namespaceElement, includeSubnamespaces);
                }
            }
        }

        protected abstract INamespace GetNamespaceElement(IStructuralMatchResult match, out bool includeSubnamespaces);
    }
}