using System.Collections.Generic;
using System.Linq;
using JetBrains.DocumentModel;
using JetBrains.ReSharper.Psi;

namespace AgentMulder.ReSharper.Domain.Registrations
{
    public class InNamespaceRegistration : BasedOnRegistrationBase
    {
        private readonly INamespace namespaceElement;
        private readonly bool includeSubnamespaces;

        public InNamespaceRegistration(DocumentRange documentRange, INamespace namespaceElement, bool includeSubnamespaces, IEnumerable<WithServiceRegistration> withServices)
            : base(documentRange, withServices)
        {
            this.namespaceElement = namespaceElement;
            this.includeSubnamespaces = includeSubnamespaces;
        }

        public override bool IsSatisfiedBy(ITypeElement typeElement)
        {
            var containingNamespace = typeElement.GetContainingNamespace();
            if (containingNamespace.QualifiedName == namespaceElement.QualifiedName)
            {
                return withServices.All(registration => registration.IsSatisfiedBy(typeElement));
            }
            return false;
        }

        public override string ToString()
        {
            return string.Format("In namespace: {0}, {1}", namespaceElement.QualifiedName,
              string.Join(", ", withServices.Select(registration => registration.ToString())));
        }
    }
}