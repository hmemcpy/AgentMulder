using JetBrains.ReSharper.Psi;

namespace AgentMulder.ReSharper.Domain.Registrations
{
    public class NegateRegistration : FilteredRegistrationBase
    {
        private readonly IComponentRegistration registration;

        public NegateRegistration(IComponentRegistration registration)
            : base(registration.RegistrationElement)
        {
            this.registration = registration;
        }

        public override bool IsSatisfiedBy(ITypeElement typeElement)
        {
            return !registration.IsSatisfiedBy(typeElement);
        }
    }
}