using System;
using JetBrains.ReSharper.Psi;
using JetBrains.ReSharper.Psi.Tree;

namespace AgentMulder.ReSharper.Domain.Registrations
{
    public class WithServiceRegistration : ComponentRegistrationBase
    {
        private readonly Predicate<ITypeElement> predicate;
        private readonly string description;

        public WithServiceRegistration(ITreeNode registrationElement, Predicate<ITypeElement> predicate, string description)
            : base(registrationElement)
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