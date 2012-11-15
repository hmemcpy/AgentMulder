using System.Collections.Generic;
using System.Linq;
using JetBrains.ReSharper.Psi;
using JetBrains.ReSharper.Psi.Tree;

namespace AgentMulder.ReSharper.Domain.Registrations
{
    internal class CompositeRegistration : ComponentRegistrationBase
    {
        private readonly ModuleBasedOnRegistration moduleBasedOnRegistration;
        private readonly IEnumerable<BasedOnRegistrationBase> basedOnRegistrations;

        public CompositeRegistration(ITreeNode registrationElement, ModuleBasedOnRegistration moduleBasedOnRegistration, IEnumerable<BasedOnRegistrationBase> basedOnRegistrations)
            : base(registrationElement)
        {
            this.moduleBasedOnRegistration = moduleBasedOnRegistration;
            this.basedOnRegistrations = basedOnRegistrations;
        }

        public override bool IsSatisfiedBy(ITypeElement typeElement)
        {
            return moduleBasedOnRegistration.IsSatisfiedBy(typeElement) && 
                   basedOnRegistrations.All(registration => registration.IsSatisfiedBy(typeElement));
        }
    }
}