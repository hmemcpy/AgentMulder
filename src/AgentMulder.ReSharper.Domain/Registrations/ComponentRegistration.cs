using AgentMulder.ReSharper.Domain.Utils;
using JetBrains.ReSharper.Psi;
using JetBrains.ReSharper.Psi.Tree;

namespace AgentMulder.ReSharper.Domain.Registrations
{
    public class ComponentRegistration : ComponentRegistrationBase
    {
        private readonly ITypeElement serviceType;

        public ITypeElement Implementation { get; set; }

        public ITypeElement ServiceType
        {
            get { return serviceType; }
        }

        public ComponentRegistration(ITreeNode registrationElement, ITypeElement serviceType, ITypeElement implementationType = null)
            : base(registrationElement)
        {
            this.serviceType = serviceType;
            Implementation = implementationType;
        }

        public override bool IsSatisfiedBy(ITypeElement typeElement)
        {
            // todo for now assume that interfaces are not supported
            if (typeElement is IInterface)
            {
                return false;
            }

            if (Implementation != null)
            {
                if (Implementation.IsGenericInterface())
                {
                    return typeElement.IsDescendantOf(Implementation);
                }

                return Implementation.Equals(typeElement);
            }

            return serviceType.Equals(typeElement);
        }

        public override string ToString()
        {
            string displayName = Implementation != null ? Implementation.GetClrName().FullName
                                                        : serviceType.GetClrName().FullName;

            return string.Format("Implemented by: {0}", displayName);
        }
    }
}