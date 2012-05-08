using System;
using System.Collections.Generic;
using System.Linq;
using JetBrains.DocumentModel;
using JetBrains.ReSharper.Psi;
using JetBrains.ReSharper.Psi.Tree;

namespace AgentMulder.ReSharper.Domain.Registrations
{
    public class BasedOnRegistration : BasedOnRegistrationBase
    {
        private readonly ITypeElement basedOnElement;
        private readonly string name;

        public ITypeElement BasedOnElement
        {
            get { return basedOnElement; }
        }

        public IEnumerable<WithServiceRegistration> WithServices
        {
            get { return withServices; }
        }

        public BasedOnRegistration(ITreeNode registrationRootElement, ITypeElement basedOnElement, IEnumerable<WithServiceRegistration> withServices)
            : base(registrationRootElement, withServices)
        {
            this.basedOnElement = basedOnElement;
            IDeclaredElement d;

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
            string format = "Based on: {0}";

            string withServicesString = String.Join(", ", (string[])withServices.Select(registration => registration.ToString()));
            
            if (!string.IsNullOrWhiteSpace(withServicesString))
            {
                format += ", {1}";
            }

            return string.Format(format, name, withServicesString);
        }
    }
}