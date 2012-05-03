using System.Collections.Generic;
using System.Linq;
using AgentMulder.Containers.CastleWindsor.Patterns.FromTypes.WithService;
using AgentMulder.ReSharper.Domain.Registrations;
using JetBrains.ReSharper.Psi;
using JetBrains.ReSharper.Psi.Services.StructuralSearch;
using JetBrains.ReSharper.Psi.Tree;

namespace AgentMulder.Containers.CastleWindsor.Patterns.FromTypes.BasedOn
{
    public abstract class InNamespaceRegistrationBasePattern : BasedOnRegistrationBasePattern
    {
        protected InNamespaceRegistrationBasePattern(IStructuralSearchPattern pattern, params WithServiceRegistrationBasePattern[] withServicePatterns)
            : base(pattern, withServicePatterns)
        {
        }

        public override IEnumerable<IComponentRegistration> GetComponentRegistrations(ITreeNode parentElement)
        {
            var match = Match(parentElement);

            if (match.Matched)
            {
                bool includeSubnamespaces;
                INamespace namespaceElement = GetNamespaceElement(match, out includeSubnamespaces);

                var withServiceRegistrations = base.GetComponentRegistrations(parentElement).OfType<WithServiceRegistration>();

                yield return new InNamespaceRegistration(parentElement.GetDocumentRange(), namespaceElement, includeSubnamespaces, withServiceRegistrations);
            }
        }


        protected abstract INamespace GetNamespaceElement(IStructuralMatchResult match, out bool includeSubnamespaces);
    }
}