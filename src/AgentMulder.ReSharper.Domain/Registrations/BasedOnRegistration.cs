using System.Collections.Generic;
using System.Linq;
using JetBrains.DocumentModel;
using JetBrains.ReSharper.Psi;

namespace AgentMulder.ReSharper.Domain.Registrations
{
    public class BasedOnRegistration : ComponentRegistrationBase
    {
        private readonly ITypeElement basedOnElement;
        private readonly IEnumerable<WithServiceRegistration> withServices;
        private readonly string name;

        public ITypeElement BasedOnElement
        {
            get { return basedOnElement; }
        }

        public IEnumerable<WithServiceRegistration> WithServices
        {
            get { return withServices; }
        }

        public BasedOnRegistration(DocumentRange documentRange, ITypeElement basedOnElement, IEnumerable<WithServiceRegistration> withServices)
            : base(documentRange)
        {
            this.basedOnElement = basedOnElement;
            // without the call ToArray(), it will fail much later while iterating.
            this.withServices = withServices.ToArray();

            name = basedOnElement.GetClrName().FullName;
        }

        public override bool IsSatisfiedBy(ITypeElement typeElement)
        {
            if (typeElement.IsDescendantOf(basedOnElement))
            {
                return withServices.All(registration => registration.IsSatisfiedBy(typeElement));
            }

            return false;
        }

        public override string ToString()
        {
            return string.Format("Based on: {0}, {1}", name, 
               string.Join(", ", withServices.Select(registration => registration.ToString())));
        }
    }
}