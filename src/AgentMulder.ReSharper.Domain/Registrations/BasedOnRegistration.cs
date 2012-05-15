using System;
using System.Collections.Generic;
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

        public BasedOnRegistration(ITreeNode registrationRootElement, ITypeElement basedOnElement, IEnumerable<WithServiceRegistration> withServices)
            : base(registrationRootElement, withServices)
        {
            this.basedOnElement = basedOnElement;

            name = basedOnElement.GetClrName().FullName;
        }

        public override bool IsSatisfiedBy(ITypeElement typeElement)
        {
            if (typeElement is IInterface)
            {
                return false;
            }

            if (!typeElement.IsDescendantOf(basedOnElement))
            {
                return false;
            }

            return base.IsSatisfiedBy(typeElement);;
        }

        public override string ToString()
        {
            string format = "Based on: {0}";

            string baseToString = base.ToString();
            
            if (!string.IsNullOrWhiteSpace(baseToString))
            {
                format += ", {1}";
            }

            return string.Format(format, name, baseToString);
        }
    }
}