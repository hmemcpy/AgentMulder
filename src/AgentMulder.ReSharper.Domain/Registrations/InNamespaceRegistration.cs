using System;
using System.Collections.Generic;
using System.Linq;
using JetBrains.DocumentModel;
using JetBrains.ReSharper.Psi;

namespace AgentMulder.ReSharper.Domain.Registrations
{
    public class InNamespaceRegistration : BasedOnRegistrationBase
    {
        private readonly INamespace matchedNamespace;
        private readonly bool includeSubnamespaces;

        public InNamespaceRegistration(DocumentRange documentRange, INamespace matchedNamespace, bool includeSubnamespaces, IEnumerable<WithServiceRegistration> withServices)
            : base(documentRange, withServices)
        {
            this.matchedNamespace = matchedNamespace;
            this.includeSubnamespaces = includeSubnamespaces;
        }

        public override bool IsSatisfiedBy(ITypeElement typeElement)
        {
            var elementNamespace = typeElement.GetContainingNamespace();

            bool isMatch;
            if (includeSubnamespaces)
            {
                isMatch = elementNamespace.QualifiedName == matchedNamespace.QualifiedName ||
                          elementNamespace.QualifiedName.StartsWith(matchedNamespace.QualifiedName + ".", StringComparison.OrdinalIgnoreCase);
            }
            else
            {
                isMatch = elementNamespace.QualifiedName == matchedNamespace.QualifiedName;
            }

            return isMatch && withServices.All(registration => registration.IsSatisfiedBy(typeElement));
        }

        public override string ToString()
        {
            return string.Format("In namespace: {0}, {1}", matchedNamespace.QualifiedName,
              string.Join(", ", withServices.Select(registration => registration.ToString())));
        }
    }
}