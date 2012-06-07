using System;
using JetBrains.ReSharper.Psi;
using JetBrains.ReSharper.Psi.Tree;

namespace AgentMulder.ReSharper.Domain.Registrations
{
    public abstract class BasedOnRegistrationBase : ComponentRegistrationBase
    {
        private Predicate<ITypeElement> filter = typeElement => !(typeElement is IInterface);

        protected BasedOnRegistrationBase(ITreeNode registrationRootElement)
            : base(registrationRootElement)
        {
        }

        protected void AddFilter(Predicate<ITypeElement> condition)
        {
            filter += condition;
        }

        public override bool IsSatisfiedBy(ITypeElement typeElement)
        {
            return filter(typeElement);
        }
    }
}