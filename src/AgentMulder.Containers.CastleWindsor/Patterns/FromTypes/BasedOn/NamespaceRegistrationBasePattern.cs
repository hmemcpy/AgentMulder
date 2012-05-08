using System.Collections.Generic;
using System.Linq;
using AgentMulder.Containers.CastleWindsor.Patterns.FromTypes.WithService;
using AgentMulder.ReSharper.Domain.Registrations;
using JetBrains.ReSharper.Psi;
using JetBrains.ReSharper.Psi.Services.StructuralSearch;
using JetBrains.ReSharper.Psi.Tree;

namespace AgentMulder.Containers.CastleWindsor.Patterns.FromTypes.BasedOn
{
    public abstract class NamespaceRegistrationBasePattern : BasedOnRegistrationBasePattern
    {
        protected NamespaceRegistrationBasePattern(IStructuralSearchPattern pattern, params WithServiceRegistrationBasePattern[] withServicePatterns)
            : base(pattern, withServicePatterns)
        {
        }

        public override IEnumerable<IComponentRegistration> GetComponentRegistrations(ITreeNode registrationRootElement)
        {
            var match = Match(registrationRootElement);

            if (match.Matched)
            {
                bool includeSubnamespaces;
                INamespace namespaceElement = GetNamespaceElement(match, out includeSubnamespaces);
                if (namespaceElement != null)
                {
                    var withServiceRegistrations = base.GetComponentRegistrations(registrationRootElement).OfType<WithServiceRegistration>();

                    yield return new InNamespaceRegistration(registrationRootElement, 
                        namespaceElement, includeSubnamespaces, withServiceRegistrations);
                }
            }
        }

        protected abstract INamespace GetNamespaceElement(IStructuralMatchResult match, out bool includeSubnamespaces);
    }
}