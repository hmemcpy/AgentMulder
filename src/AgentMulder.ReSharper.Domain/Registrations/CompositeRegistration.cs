using System;
using System.Collections.Generic;
using System.Linq;
using JetBrains.ReSharper.Psi;
using JetBrains.ReSharper.Psi.Tree;

namespace AgentMulder.ReSharper.Domain.Registrations
{
    public class CompositeRegistration : ComponentRegistrationBase
    {
        private readonly IEnumerable<IComponentRegistration> componentRegistrations;

        public CompositeRegistration(ITreeNode registrationElement, IEnumerable<IComponentRegistration> componentRegistrations)
            : base(registrationElement)
        {
            this.componentRegistrations = componentRegistrations;
        }

        public override bool IsSatisfiedBy(ITypeElement typeElement)
         {
            return componentRegistrations.All(registration => registration.IsSatisfiedBy(typeElement));
        }

        public override string ToString()
        {
            return string.Join(Environment.NewLine, componentRegistrations.Select(registration => registration.ToString()));
        }
    }
}