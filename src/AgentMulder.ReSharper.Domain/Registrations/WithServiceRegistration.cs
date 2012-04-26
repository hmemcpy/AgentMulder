using System;
using JetBrains.DocumentModel;
using JetBrains.ReSharper.Psi;

namespace AgentMulder.ReSharper.Domain.Registrations
{
    public class WithServiceRegistration : ComponentRegistrationBase
    {
        private readonly Predicate<ITypeElement> predicate;
        private readonly string description;

        public WithServiceRegistration(DocumentRange documentRange, Predicate<ITypeElement> predicate, string description)
            : base(documentRange)
        {
            this.predicate = predicate;
            this.description = description;
        }

        public override bool IsSatisfiedBy(ITypeElement typeElement)
        {
            return predicate(typeElement);
        }

        public override string ToString()
        {
            return string.Format("With Service: {0}", description);
        }
    }
}