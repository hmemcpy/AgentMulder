using System;
using JetBrains.DocumentModel;
using JetBrains.ReSharper.Psi;

namespace AgentMulder.ReSharper.Domain.Registrations
{
    public class WithServiceRegistration : ComponentRegistrationBase
    {
        private readonly Predicate<ITypeElement> predicate;

        public WithServiceRegistration(DocumentRange documentRange, Predicate<ITypeElement> predicate)
            : base(documentRange)
        {
            this.predicate = predicate;
        }

        public override bool IsSatisfiedBy(ITypeElement typeElement)
        {
            return predicate(typeElement);
        }
    }
}