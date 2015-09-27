using JetBrains.ReSharper.Psi;
using JetBrains.ReSharper.Psi.Tree;

namespace AgentMulder.ReSharper.Domain.Registrations
{
    public class HasAttributeRegistration : FilteredRegistrationBase
    {
        private readonly ITypeElement attributeType;

        public HasAttributeRegistration(ITreeNode registrationElement, ITypeElement attributeType)
            : base(registrationElement)
        {
            this.attributeType = attributeType;
        }

        public override bool IsSatisfiedBy(ITypeElement typeElement)
        {
            var attributesOwner = typeElement as IAttributesOwner;
            if (attributesOwner == null)
            {
                return false;
            }

            return attributesOwner.HasAttributeInstance(attributeType.GetClrName(), true);
        }
    }
}