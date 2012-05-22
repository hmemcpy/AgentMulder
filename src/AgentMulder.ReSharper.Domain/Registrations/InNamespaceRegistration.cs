using System;
using JetBrains.ReSharper.Psi;
using JetBrains.ReSharper.Psi.Tree;

namespace AgentMulder.ReSharper.Domain.Registrations
{
    public class InNamespaceRegistration : BasedOnRegistrationBase
    {
        private readonly INamespace matchedNamespace;
        private readonly bool includeSubnamespaces;

        public InNamespaceRegistration(ITreeNode registrationRootElement, INamespace matchedNamespace, bool includeSubnamespaces)
            : base(registrationRootElement)
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

            return isMatch && base.IsSatisfiedBy(typeElement);
        }

        public override string ToString()
        {
            return string.Format("In namespace: {0}, {1}", matchedNamespace.QualifiedName, base.ToString());

        }
    }
}