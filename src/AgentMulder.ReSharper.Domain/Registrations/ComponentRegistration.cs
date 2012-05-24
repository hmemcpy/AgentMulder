using AgentMulder.ReSharper.Domain.Utils;
using JetBrains.ReSharper.Psi;
using JetBrains.ReSharper.Psi.Tree;

namespace AgentMulder.ReSharper.Domain.Registrations
{
    public class ComponentRegistration : ComponentRegistrationBase
    {
        private readonly ITypeElement serviceType;
        private ITypeElement implementation;

        public ITypeElement Implementation
        {
            get { return implementation; }
            set { implementation = value; }
        }

        public ITypeElement ServiceType
        {
            get { return serviceType; }
        }

        public ComponentRegistration(ITreeNode registrationElement, ITypeElement serviceType)
            : base(registrationElement)
        {
            this.serviceType = serviceType;
        }

        public override bool IsSatisfiedBy(ITypeElement typeElement)
        {
            // todo for now assume that interfaces are not supported
            if (typeElement is IInterface)
            {
                return false;
            }

            if (implementation != null)
            {
                if (implementation.IsGenericInterface())
                {
                    return typeElement.IsDescendantOf(implementation);
                }

                return implementation.Equals(typeElement);
            }

            return serviceType.Equals(typeElement);
        }

        public override string ToString()
        {
            string displayName = implementation != null ? implementation.GetClrName().FullName
                                                        : serviceType.GetClrName().FullName;

            return string.Format("Implemented by: {0}", displayName);
        }
    }
}